using System;
using System.Threading;

namespace WaitCallbackWithObject
{
    public class TaskInfo
    {
        public string BoilerPlate;
        public int value;
        public TaskInfo(string text, int number) {
            BoilerPlate = text;
            value = number;
        }
    }
    public class Example
    {
        static void Main()
        {
            TaskInfo ti = new TaskInfo("Hi this is Sean's string", 5);
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), ti);
            ti.BoilerPlate = "I changed the string to something refrencing inline code ";
            ti.value = 3;
            //Here is how you can put the method functionality inline. This code: obj => 
            //Console.WriteLine("Writing from inline method {0} {1}",ti.BoilerPlate, ti.value) is called 
            //a lambda expression
            ThreadPool.QueueUserWorkItem(obj => Console.WriteLine("Writing from inline method {0} {1}",ti.BoilerPlate, ti.value),ti);
            ThreadPool.QueueUserWorkItem( obj => Console.WriteLine("Writing from inline method {0} {1}" + obj), ti);
            //You can also see what happens if you print obj. Just uncomment the line below. It prints 
            //the namespace.classname i.e. WaitCallbackWithObject.TaskInfo
            //ThreadPool.QueueUserWorkItem(obj => Console.WriteLine("Writing from inline method {0} {1}" + obj), ti);
            Console.WriteLine("Main thread does some work and then it exits");
            Thread.Sleep(1000);
            Console.WriteLine("Main Thread Exits");
            Console.ReadLine();
        }
        static void ThreadProc (object stateInfo) {
            TaskInfo ti = (TaskInfo) stateInfo;
            Console.WriteLine(ti.BoilerPlate, ti.value);
            Console.ReadLine();
        }
    }
}
