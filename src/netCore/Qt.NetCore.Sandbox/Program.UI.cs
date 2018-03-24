using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Qt.NetCore.Sandbox
{
    class Program
    {
        public class AnotherType
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

            public void Test()
            {
                Console.WriteLine("AnotherType: Test() #" + _COUNTER.ToString());
            }
        }

        public class TestQmlImport
        {
            private static int _COUNTER = 0;
            public TestQmlImport()
            {
                _COUNTER++;
                Console.WriteLine("TestQmlImport: Ctor() #" + _COUNTER.ToString());
            }

            ~TestQmlImport()
            {
                Console.WriteLine("TestQmlImport: ~Dtor() #" + _COUNTER.ToString());
            }

            public AnotherType Create()
            {
                Console.WriteLine("AnotherType: Create() #" + _COUNTER.ToString());
                return new AnotherType();
            }
            
            public void TestMethod(AnotherType anotherType)
            {
                Console.WriteLine("AnotherType: TestMethod() #" + _COUNTER.ToString());
            }

            public void OnPressed(string editContent)
            {
                Console.Out.WriteLine("Message from QML: OnPressed, editContent = {0}", editContent);
            }
        }

        static int Main()
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Helpers.LoadDebugVariables();
            }

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(10);
                    GC.Collect(GC.MaxGeneration);
                }
            // ReSharper disable FunctionNeverReturns
            });
            // ReSharper restore FunctionNeverReturns

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
