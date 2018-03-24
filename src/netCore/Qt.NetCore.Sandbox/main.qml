import QtQuick 2.7
import QtQuick.Controls 2.0
import QtQuick.Layouts 1.0
import test 1.1

ApplicationWindow {
    visible: true
    width: 640
    height: 480
    title: qsTr("Hello World")

	Item {
		Timer {
			interval: 1000; running: true; repeat: true
			onTriggered: {
				var par = test.Create()
				test.TestMethod(par)
				gc()
			}
		}
	}

	Page1 {
		anchors.horizontalCenter: parent.horizontalCenter
		button1.onClicked: {
			//console.log("Button Pressed. Entered text: " + textField1.text);
			test.OnPressed(textField1.text);
		}

	}

	Text {
		id: textt
	}

	TestQmlImport {
		id: test
	}
}
