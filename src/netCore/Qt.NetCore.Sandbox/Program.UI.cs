using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Qt.NetCore.Sandbox
{
    class Program
    {
        public class AnotherType : INotifyPropertyChanged
        {
            private static int _COUNTER = 0;
            public AnotherType()
            {
                _COUNTER++;
                Console.WriteLine("AnotherType: Ctor() #" + _COUNTER.ToString());
            }

            ~AnotherType()
            {
                Console.WriteLine("AnotherType: ~Dtor() #" + _COUNTER.ToString());
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void Test()
            {
                Console.WriteLine("AnotherType: Test() #" + _COUNTER.ToString());
            }
        }

        public class TestQmlImport : INotifyPropertyChanged
        {
            private static int _ANOTHER_TYPE_COUNTER = 0;

            private String _MessageToSend = "TestMessage äöü";

            public event PropertyChangedEventHandler PropertyChanged;

            public String MessageToSend
            {
                get
                {
                    return _MessageToSend;
                }
                set
                {
                    if(!object.Equals(_MessageToSend, value))
                    {
                        _MessageToSend = value;
                        RaiseNotifyPropertyChanged();
                    }
                }
            }
            public TestQmlImport()
            {
                Console.WriteLine("TestQmlImport: Ctor()");
            }

            ~TestQmlImport()
            {
                Console.WriteLine("TestQmlImport: ~Dtor()");
            }

            public AnotherType Create()
            {
                _ANOTHER_TYPE_COUNTER++;
                Console.WriteLine("AnotherType: Create() #" + _ANOTHER_TYPE_COUNTER.ToString());
                MessageToSend = string.Format("Hey there! We already created {0} instances!", _ANOTHER_TYPE_COUNTER);
                return new AnotherType();
            }

            public void TestMethod(AnotherType anotherType)
            {
                Console.WriteLine("AnotherType: TestMethod()");
            }

            public void OnPressed(string editContent)
            {
                Console.Out.WriteLine("Message from QML: OnPressed, editContent = {0}", editContent);
            }

            public void SendMessage()
            {
                Console.Out.WriteLine("Received a Message from QML: {0}", MessageToSend);
            }

            private void RaiseNotifyPropertyChanged([CallerMemberName]string propertyName = "")
            {
                if(PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        static int Main()
        {
            if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Helpers.LoadDebugVariables();
            }
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        Thread.Sleep(10);
            //        GC.Collect(GC.MaxGeneration);
            //    }
            //// ReSharper disable FunctionNeverReturns
            //});
            //// ReSharper restore FunctionNeverReturns

            using (var r = new StringVector(0))
            {
                using (var app = new QGuiApplication(r))
                {
                    using (var engine = new QQmlApplicationEngine())
                    {
                        QQmlApplicationEngine.RegisterType<TestQmlImport>("test", 1, 1);
                        
                        engine.loadFile("main.qml");
                        return app.exec();
                    }
                }
            }
        }
    }
}
