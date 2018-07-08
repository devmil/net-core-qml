using System;
using System.Collections.Generic;
using System.Text;

namespace Qt.NetCore
{
    class QtGuiThreadDispatcher : GuiThreadContextTriggerCallback
    {
        private Queue<Action> m_actionQueue = new Queue<Action>();

        public void Dispatch(Action action)
        {
            lock (m_actionQueue)
            {
                m_actionQueue.Enqueue(action);
            }
        }

        public override void onGuiThreadContextTrigger()
        {
            Action action = null;
            lock (m_actionQueue)
            {
                action = m_actionQueue.Dequeue();
            }
            if (action != null)
            {
                action.Invoke();
            }
        }
    }

    public partial class QGuiApplication
    {
        private QtGuiThreadDispatcher m_dispatcher = new QtGuiThreadDispatcher();

        partial void OnCreate()
        {
            Callback.Instance.SetApp(this);
            EnsureDispatcherRegistration();
        }

        public void InvokeOnGuiThread(Action action)
        {
            EnsureDispatcherRegistration();
            m_dispatcher.Dispatch(action);
            requestGuiThreadContextTrigger();
        }

        private bool _IsRegistered = false;
        private void EnsureDispatcherRegistration()
        {
            if (_IsRegistered)
            {
                return;
            }
            _IsRegistered = true;
            setGuiThreadContextTriggerCallback(m_dispatcher);
        }
    }
}
