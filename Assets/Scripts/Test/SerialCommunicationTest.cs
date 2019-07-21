using System.Threading;
using UnityEngine;

public class SerialCommunicationTest : MonoBehaviour
{
    [SerializeField]
    string portName;

    [SerializeField]
    int baudRate = 9600;

    SerialCommunicationHandler handler;

    Thread thread;

    void Start()
    {
        handler = new SerialCommunicationHandler(portName, baudRate);
        handler.OnDataReceived += OnDataReceived;

        thread = new Thread(handler.ReadLineAndHandleEvent);
        thread.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            handler.Write("1");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            handler.Write("0");
        }
    }

    void OnApplicationQuit()
    {
        if (handler != null)
        {
            handler.CloseAndDispose();
        }

        if (thread != null && thread.IsAlive)
        {
            thread.Join();
        }
    }

    void OnDataReceived(string message)
    {
        Debug.Log(message);
    }
}
