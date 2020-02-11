using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;
using System.Reflection;
using System.Runtime.Remoting;
using System.Windows.Forms;

namespace Trustworthy_Coursework
{
    class Sandboxer : MarshalByRefObject
    {
        static string pathToUntrusted = @"..\..\..\UntrustedCode\bin\Debug";
        static string untrustedAssembly = "UntrustedCode";
        static string untrustedClass = "";//"UntrustedCode.UntrustedClass";
        static string entryPoint = "Main";//"IsFibonacci";
        private static Object[] parameters = { 45 };

        /*
        public Sandboxer(string PathGiven, string FileName, string Class)
        {
            pathToUntrusted = PathGiven;
            untrustedAssembly = FileName;
            Main();
        }*/

        public void Start(string PathGiven, string FileName, string Class)
        {
            pathToUntrusted = @PathGiven;
            untrustedAssembly = FileName;
            untrustedClass = "";
            entryPoint = "Main";
            try
            {              
                //Setting the AppDomainSetup. It is very important to set the ApplicationBase to a folder   
                //other than the one in which the sandboxer resides.  
                AppDomainSetup adSetup = new AppDomainSetup();
                adSetup.ApplicationBase = Path.GetFullPath(pathToUntrusted);

                //Setting the permissions for the AppDomain. We give the permission to execute and to   
                //read/discover the location where the untrusted code is loaded.
                PermissionSet permSet = SetupPermissions();

                //We want the sandboxer assembly's strong name, so that we can add it to the full trust list.  
                StrongName fullTrustAssembly = typeof(Sandboxer).Assembly.Evidence.GetHostEvidence<StrongName>();

                //Now we have everything we need to create the AppDomain, so let's create it.  
                AppDomain newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly);
             
                //Use CreateInstanceFrom to load an instance of the Sandboxer class into the  
                //new AppDomain.   
                ObjectHandle handle = Activator.CreateInstanceFrom(
                    newDomain, typeof(Sandboxer).Assembly.ManifestModule.FullyQualifiedName,
                    typeof(Sandboxer).FullName);

                //Unwrap the new domain instance into a reference in this domain and use it to execute the   
                //untrusted code.  
                Sandboxer newDomainInstance = (Sandboxer)handle.Unwrap();
                newDomainInstance.ExecuteUntrustedCode(untrustedAssembly, untrustedClass, entryPoint, parameters, @PathGiven.Trim());
            }

            catch (Exception ex)
            {
                DialogResult dresult = MessageBox.Show(ex.ToString(), "Encountered error while running sandbox",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dresult == DialogResult.OK)
                {
                    return; //return to client
                }
            }
}

        public void ExecuteUntrustedCode(string assemblyName, string typeName, string entryPoint, Object[] parameters, string PathGiven)
        {
            //Load the MethodInfo for a method in the new Assembly. This might be a method you know, or   
            //you can use Assembly.EntryPoint to get to the main function in an executable.  

            //string[] dll = Directory.GetFiles(Environment.CurrentDirectory, assemblyName + "*.dll");
            PathGiven = PathGiven.Replace(@"\\", @"\");
            Assembly A = Assembly.LoadFile(@PathGiven);
            MethodInfo target = null;


            // try and find the type so we can get its class name
            //object here = A.DefinedTypes;            
            Type[] Ass_Types = A.GetTypes();                 
           

            // try to find some methods   
            object [,] MethodData = new object[A.DefinedTypes.Count(),2];
            MethodInfo[][] metInfo = new MethodInfo[A.DefinedTypes.Count()][];

            for (int i = 0; i < A.DefinedTypes.Count(); i++)
            {
                string _type = Ass_Types[i].FullName;
                metInfo[i] = A.GetType(_type).GetMethods();
            }
            int counter = 0;
            foreach (Type t in Ass_Types)
            {
                MethodData[counter, 1] = t;
                counter++;
            }

            //MethodInfo[] m = A.GetType(_type).GetMethods();
            if (MethodData.Length != 0)
            {             
                    // create new form where use can select a method to run
                    try
                    {
                        Method_List_Form form = new Method_List_Form(MethodData, metInfo);
                        Application.Run(form);
                    }
                    catch (Exception ex)
                    {
                        // When we print informations from a SecurityException extra information can be printed if we are   
                        //calling it with a full-trust stack.  
                        new PermissionSet(PermissionState.Unrestricted).Assert();
                        Console.WriteLine("SecurityException caught:\n{0}", ex.ToString());
                        CodeAccessPermission.RevertAssert();
                        Console.ReadLine();
                        Failed(ex);
                    }            
            }
        }

        public static void Failed(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Failed", MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        /// <summary>
        /// //Setting the permissions for the AppDomain. We give the permission to execute and to   
            //read/discover the location where the untrusted code is loaded.
        /// </summary>
        /// <returns></returns>
        static public PermissionSet SetupPermissions()
        {               
            PermissionSet permSet = new PermissionSet(PermissionState.Unrestricted);
            //permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));

            
            //The unrestricted state of the permission.
            //permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.AllFlags));
           

            //Ability to create and manipulate an AppDomain.
            //permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.ControlAppDomain));

            //Ability to use certain advanced operations on threads.
            //permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.ControlAppDomain));

            //No security access.
            //permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.NoFlags));

            //Ability to call unmanaged code.
            //Since unmanaged code potentially allows other permissions to be bypassed, this is a dangerous permission that should only be granted to highly trusted code. It is used for such applications as calling native code using PInvoke or using COM interop.
            //permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));

            //*/
            return permSet;
        }

    }
}
