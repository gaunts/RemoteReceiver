using RemoteReceiver.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace RemoteReceiver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            base.OnStartup(e);

            var assembly = Assembly.GetExecutingAssembly();
            PreferencesManager.CorrectAutoLaunchPath(assembly.Location);
            var sysTray = new SysTray();
            sysTray.BeginInit();

            RemoteWebListener.WebCommandExecutionReceived += SocketCommandExecution.ExecuteCommand;
            RemoteWebListener.StartListening();
            RemoteSerialListener.StartListening();

            //ConfigurationHelper.ShowConfigurationWindow();
        }

        private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            try
            {
                AssemblyName assemblyName = new AssemblyName(args.Name);

                var path = assemblyName.Name + ".dll";
                if (assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture) == false)
                    path = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path);

                using (Stream stream = executingAssembly.GetManifestResourceStream(path))
                {
                    if (stream == null) return null;

                    var assemblyRawBytes = new byte[stream.Length];
                    stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                    return Assembly.Load(assemblyRawBytes);
                }
            }
            catch
            {
                Console.WriteLine("Could not generate exe file");
            }
            return executingAssembly;
        }
    }
}
