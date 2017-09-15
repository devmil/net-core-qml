using System;
using Xunit;

namespace Qt.NetCore.Tests
{
    public class EventHandlerTests : BaseTests<EventHandlerTests.EventHandlerTestsQml>
    {
		public class EventHandlerTestsQml
		{
			public virtual uint Property { get; set; }

            public event EventHandler GenericEvent;

            public void RaiseGenericEvent()
            {
                var handler = GenericEvent;
                if (handler != null)
                    handler(this, EventArgs.Empty);
            }
		}

        [Fact]
        public void Can_raise_event()
        {
			NetTestHelper.RunQml(qmlApplicationEngine,
				@"
                import QtQuick 2.0
                import tests 1.0

                EventHandlerTestsQml {
                    id: test
                    Component.onCompleted: function() {
                        console.log(""TEST"")
                        test.GenericEvent.connect(testHandler)
                        console.log(""CONNECTED"")
                        //test.RaiseGenericEvent()
                    }

                    function testHandler(person, notice) {
                        
                    }
                }
            ");
        }
    }
}
