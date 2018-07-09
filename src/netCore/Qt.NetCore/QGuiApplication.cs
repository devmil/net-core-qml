using System;
using System.Collections.Generic;
using System.Text;

namespace Qt.NetCore
{
    interface IRequestGuiThreadContextHandler
    {
        void RequestGuiThreadContextTrigger();
    }

    class QtGuiThreadDispatcher : GuiThreadContextTriggerCallback
    {
        private Queue<Action> _ActionQueue = new Queue<Action>();
        private IRequestGuiThreadContextHandler _RequestGuiThreadContextHandler;

        public QtGuiThreadDispatcher(IRequestGuiThreadContextHandler requestGuiThreadContextHandler)
        {
            _RequestGuiThreadContextHandler = requestGuiThreadContextHandler;
        }

        public void Dispatch(Action action)
        {
            lock (_ActionQueue)
            {
                _ActionQueue.Enqueue(action);
            }
            _RequestGuiThreadContextHandler.RequestGuiThreadContextTrigger();
        }

        public override void onGuiThreadContextTrigger()
        {
            Action action = null;
            lock (_ActionQueue)
            {
                action = _ActionQueue.Dequeue();
            }
            if (action != null)
            {
                action.Invoke();
            }
        }
    }

    public partial class QGuiApplication : IUiContext, IRequestGuiThreadContextHandler
    {
        private QtGuiThreadDispatcher _Dispatcher;

        partial void OnCreate()
        {
            _Dispatcher = new QtGuiThreadDispatcher(this);
            setGuiThreadContextTriggerCallback(_Dispatcher);
            Callback.Instance.SetUiContext(this);
        }

        public void InvokeOnGuiThread(Action action)
        {
            _Dispatcher.Dispatch(action);
        }

        public void RequestGuiThreadContextTrigger()
        {
            requestGuiThreadContextTrigger();
        }
    }
}
