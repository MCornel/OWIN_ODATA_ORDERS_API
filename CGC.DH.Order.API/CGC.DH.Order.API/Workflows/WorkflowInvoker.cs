using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;

namespace CGC.DH.Order.API.Workflows
{
    public class WorkflowInvoker
    {
        public static void Invoke(IWorkflow workflow)
        {
            workflow = new Workflow();
            workflow.Add(new CallWebService(1));
            workflow.Add(new CallWebService(2));
            workflow.Add(new CallWebService(3));
            workflow.Add(new CallWebService(4));
            workflow.Add(new CallWebService(5));

            var engine = new WorkflowEngine();
            engine.Run(workflow);
        }

        public static async Task InvokeAsync(IWorkflow workflow)
        {
            workflow = new Workflow();
            workflow.Add(new CallWebService(1));
            workflow.Add(new CallWebService(2));
            workflow.Add(new CallWebService(3));
            workflow.Add(new CallWebService(4));
            workflow.Add(new CallWebService(5));

            var engine = new WorkflowEngine();
            await engine.RunAsync(workflow);
        }
    }

    class CallWebService : ITask
    {
        int _id;

        public CallWebService(int id)
        {
            _id = id;
        }
        public void Execute()
        {
            System.Diagnostics.Debug.WriteLine("Begin Calling web service... " + _id);

            if (_id == 2 || _id == 4)
                throw new Exception("Thrown from id# " + _id);

            Thread.Sleep(2000);
            System.Diagnostics.Debug.WriteLine("End Calling web service... " + _id);
        }

        public async Task ExecuteAsync()
        {
            //bool b = await Task.Run(async() => 
            //{
                //Console.WriteLine("Calling web service async...");
                System.Diagnostics.Debug.WriteLine("Begin Calling web service... " + _id);

                if (_id == 2 || _id == 4)
                    throw new Exception("Thrown from id# " + _id);

                await Task.Delay(2000);
                System.Diagnostics.Debug.WriteLine("End Calling web service... " + _id);
            //    return true;
            //});
        }
    }

    public interface ITask
    {
        void Execute();
        Task ExecuteAsync();
    }

    public interface IWorkflow
    {
        void Add(ITask task);
        void Remove(ITask task);
        IEnumerable<ITask> GetTasks();
    }

    public class Workflow : IWorkflow
    {
        private readonly List<ITask> _tasks;

        public Workflow()
        {
            _tasks = new List<ITask>();
        }

        public void Add(ITask task)
        {
            _tasks.Add(task);
        }

        public void Remove(ITask task)
        {
            _tasks.Remove(task);
        }

        public IEnumerable<ITask> GetTasks()
        {
            return _tasks;
        }
    }

    public class WorkflowEngine
    {
        public void Run(IWorkflow workflow)
        {
            foreach (ITask task in workflow.GetTasks()) //.Where(t => t.ToString().Contains("CallWebService"))
            {
                try
                {
                    task.Execute();
                }
                catch (Exception exc)
                {
                    // Logging
                    // Terminate and persist the state of the workflow
                    System.Diagnostics.Debug.WriteLine("Exception: " + exc.Message);
                    //throw;
                }
            }
        }

        public async Task RunAsync(IWorkflow workflow)
        {
            Task[] asyncOps = (from tasks in workflow.GetTasks() select tasks.ExecuteAsync()).ToArray(); //Task<string>[] 

            try
            {
                await Task.WhenAll(asyncOps);
                //string[] pages = await Task.WhenAll(asyncOps);
                
            }
            catch (Exception exc)
            {
                //foreach (Task<string> faulted in asyncOps.Where(t => t.IsFaulted))
                foreach (Task faulted in asyncOps.Where(t => t.IsFaulted))
                {
                    // work with faulted and faulted.Exception
                    System.Diagnostics.Debug.WriteLine("Exception: " + faulted.Exception.Message + " ... InnerException: " + faulted.Exception.InnerException.Message);
                }
            }
        }       
    }
}