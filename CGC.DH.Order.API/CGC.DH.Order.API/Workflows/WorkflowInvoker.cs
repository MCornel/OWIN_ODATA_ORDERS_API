using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace CGC.DH.Order.API.Workflows
{
    public class WorkflowInvoker
    {
        static void Invoke(IWorkflow workflow)
        {
            //var workflow = new Workflow();
            //workflow.Add(new CallWebService());
            //workflow.Add(new CallWebService());

            var engine = new WorkflowEngine();
            engine.Run(workflow);
        }

        static async Task InvokeAsync(IWorkflow workflow)
        {
            //var workflow = new Workflow();
            //workflow.Add(new CallWebService());
            //workflow.Add(new CallWebService());

            var engine = new WorkflowEngine();
            await engine.RunAsync(workflow);
        }
    }

    class CallWebService : ITask
    {
        public void Execute()
        {
            Console.WriteLine("Calling web service...");
        }

        public async Task ExecuteAsync()
        {            
            //bool b = await Task.Run(async() => 
            //{
                Console.WriteLine("Calling web service async...");           
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
            foreach (ITask task in workflow.GetTasks())
            {
                try
                {
                    task.Execute();
                }
                catch (Exception)
                {
                    // Logging
                    // Terminate and persist the state of the workflow
                    throw;
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
                }
            }
        }       
    }
}