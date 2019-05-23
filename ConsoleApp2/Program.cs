using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        [STAThread]
        private static async Task Main(string[] args)
        {
            Console.WriteLine("@@@@@ Main Test");
            for (int index = 0; index < 10; ++index)
            {
                Console.WriteLine("{0} Test ----", index);
                await Task.Factory.StartNew(() => 
                {
                    Activity.Create(Guid.NewGuid());
                    Console.WriteLine("Main instance1::{0}", Activity.Instance.ID);
                    Activity.Create(Guid.NewGuid());
                    Console.WriteLine("Main instance2::{0}", Activity.Instance.ID);
                }).ConfigureAwait(false);
            }
            Program2 program = new Program2();
            program.TestSingletonAsync();
            Console.ReadKey();
        }
    }

    class Program2
    {
        public async void TestSingletonAsync()
        {
            Console.WriteLine("@@@@@ TestSingleton Test");
            await Task.Factory.StartNew(() =>
            {
                Activity.Create(Guid.NewGuid());
                Console.WriteLine("TestSingleton instance1::{0}", Activity.Instance.ID);
                Activity.Create(Guid.NewGuid());
                Console.WriteLine("TestSingleton instance2::{0}", Activity.Instance.ID);
            }).ConfigureAwait(false);
        }
    }


    public interface IActivity
    {
        string GetBranchName(int activityID);
    }

    public abstract class AbstractActivity : IActivity
    {
        protected IDictionary<int, string> activityDictionary;

        protected abstract void SetData(IDictionary<int, string> activtys);

        public abstract string GetBranchName(int activityID);
    }

    public sealed class Activity : AbstractActivity
    {
        private static readonly object lazyLock = new object();
        private static Lazy<Activity> instance;

        public Guid ID { get; private set; }

        private Activity(Guid id)
        {
            this.ID = id;
            Console.WriteLine("Created instance ::" + ID);
        }

        public static Activity Instance => instance.Value;

        public static Activity Create(Guid id)
        {
            lock (lazyLock)
            {
                //Guid id = (Guid)((IEnumerable<object>)args).SingleOrDefault();
                if (instance == null)
                {
                    instance = new Lazy<Activity>(() => new Activity(id));
                }

                return instance.Value;
            }
        }

        public override string GetBranchName(int activityID)
        {
            throw new NotImplementedException();
        }

        protected override void SetData(IDictionary<int, string> activtys)
        {
            throw new NotImplementedException();
        }
    }

    public static class SingletonFactory
    {
        private static readonly IDictionary<Type, object> instances = new Dictionary<Type, object>();

        public static T Create<T>(params object[] args)
        {
            Type index = typeof(T);
            T instance;
            if (instances.ContainsKey(index))
            {
                instance = (T)instances[index];
            }
            else
            {
                instance = (T)Activator.CreateInstance(index, args);
                instances.Add(index, (object)instance);
            }
            return instance;
        }
    }
}
