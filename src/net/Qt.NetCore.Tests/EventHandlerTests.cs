using System;
using Moq;
using Xunit;

namespace Qt.NetCore.Tests
{
    public class EventHandlerTests : BaseTests<EventHandlerTests.EventHandlerTestsQml>
    {
		public class EventHandlerTestsQml
		{
			public virtual uint Property { get; set; }

            public event Action GenericEvent;

            public void RaiseGenericEvent()
            {
                var handler = GenericEvent;
                handler?.Invoke();
            }

		    public virtual void TestMethod()
		    {
		        
		    }
		}

        [Fact]
        public void Can_trigger_signal_from_qml()
        {
			NetTestHelper.RunQml(qmlApplicationEngine,
                @"
                import QtQuick 2.0
                import tests 1.0

                EventHandlerTestsQml {
                    id: test
                    Component.onCompleted: function() {
                        test.GenericEvent.connect(testHandler)
                        test.GenericEvent()
                    }

                    function testHandler() {
                        test.TestMethod()
                    }
                }
            ");

            Mock.Verify(x => x.TestMethod(), Times.Once);
        }
    }
}
