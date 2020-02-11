using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVM.SimpleMachineLanguage;
using SVM.VirtualMachine;
using Moq;
using System.Collections;

namespace SVMUnitTests
{
    [TestClass]
    public class Conditionals_Tests
    {
        Mock<IVirtualMachine> VirtualMachine;

        [TestInitialize]
        public void TestInit()
        {
            this.VirtualMachine = new Mock<IVirtualMachine>();
            VirtualMachine.SetupAllProperties();

            Stack Stacker = new Stack();
            VirtualMachine.Object.Stack = Stacker;

            int StackValue = 5;
            VirtualMachine.Object.Stack.Push(StackValue);

            Hashtable Labels = new Hashtable();
            Labels.Add("%AddOne%", "0");
            Labels.Add("%DecrOne%", "2");
            Labels.Add("%Write%", "6");
            Labels.Add("%Add%", "7");
        }


        //Equint tests --------------------------------
        [TestMethod]
        public void Equint_when_equal()
        {
            //Arrange                          
            Equint Equint_method = new Equint();
            Equint_method.VirtualMachine = VirtualMachine.Object;
            string [] Operands = new string[2] { "5", "%AddOne%" };

            Equint_method.Operands = Operands;
            string ExpectedValue = "%AddOne%";

            //Act
            Equint_method.Run(); //run instruction 
            string actual = Equint_method.VirtualMachine.Stack.Pop().ToString(); // get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual, ExpectedValue);
        }

        [TestMethod]
        public void Equint_when_notEqual()
        {
            //Arrange                          
            Equint Equint_method = new Equint();
            Equint_method.VirtualMachine = VirtualMachine.Object;
            string[] Operands = new string[2] { "1", "%AddOne%" };

            Equint_method.Operands = Operands;
            string ExpectedValue = "5";

            //Act
            Equint_method.Run(); //run instruction 
            string actual = Equint_method.VirtualMachine.Stack.Pop().ToString(); // get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual, ExpectedValue);
        }


        [TestMethod]
        public void Equint_not_int_onStack()
        {
            //Arrange                          
            Equint Equint_method = new Equint();
            VirtualMachine.Object.Stack.Push("five");
            Equint_method.VirtualMachine = VirtualMachine.Object;
            string[] Operands = new string[2] { "1", "%AddOne%" };

            Equint_method.Operands = Operands;

            //Act
            try
            {
                Equint_method.Run(); //run instruction, should throw error
            }
            catch
            {         
                return;
            }            
            Assert.Fail();
        }




        //Notequ tests --------------------------------
        [TestMethod]
        public void Notequ_when_equal()
        {
            //Arrange                          
            Notequ Notequ_method = new Notequ();
            VirtualMachine.Object.Stack.Push(5);
            Notequ_method.VirtualMachine = VirtualMachine.Object;           
            string[] Operands = new string[1] {"%AddOne%" };

            Notequ_method.Operands = Operands;
            string ExpectedValue = "5";

            //Act
            Notequ_method.Run(); //run instruction 
            string actual = Notequ_method.VirtualMachine.Stack.Pop().ToString(); // get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual, ExpectedValue);
        }


        [TestMethod]
        public void Notequ_when_notEqual()
        {
            //Arrange                          
            Notequ Notequ_method = new Notequ();
            VirtualMachine.Object.Stack.Push(4);
            Notequ_method.VirtualMachine = VirtualMachine.Object;          
            string[] Operands = new string[1] {"%AddOne%" };

            Notequ_method.Operands = Operands;
            string ExpectedValue = "%AddOne%";

            //Act
            Notequ_method.Run(); //run instruction 
            string actual = Notequ_method.VirtualMachine.Stack.Pop().ToString();// get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual, ExpectedValue);
        }


        [TestMethod]
        public void Notequ_not_int_onStack()
        {
            //Arrange                          
            Notequ Notequ_method = new Notequ();
            VirtualMachine.Object.Stack.Push("five");
            Notequ_method.VirtualMachine = VirtualMachine.Object;
            string[] Operands = new string[2] { "1", "%AddOne%" };

            Notequ_method.Operands = Operands;

            //Act
            try
            {
                Notequ_method.Run(); //run instruction, should throw error
            }
            catch
            {
                return;
            }
            Assert.Fail();
        }


        //Bltint tests --------------------------------
        [TestMethod]
        public void Bltint_not_less()
        {
            //Arrange                          
            Bltint Bltint_method = new Bltint();
            Bltint_method.VirtualMachine = VirtualMachine.Object;
            string[] Operands = new string[2] { "6", "%AddOne%" };

            Bltint_method.Operands = Operands;
            string ExpectedValue = VirtualMachine.Object.Stack.Peek().ToString();

            //Act
            Bltint_method.Run(); //run instruction 
            string actual = Bltint_method.VirtualMachine.Stack.Pop().ToString(); // get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual, ExpectedValue);
        }

        [TestMethod]
        public void Bltint_when_less()
        {
            //Arrange                          
            Bltint Bltint_method = new Bltint();
            Bltint_method.VirtualMachine = VirtualMachine.Object;
             
            Bltint_method.Operands = new string[2] { "4", "%AddOne%" };
            string ExpectedValue = "%AddOne%";

            //Act
            Bltint_method.Run(); //run instruction 
            string actual = Bltint_method.VirtualMachine.Stack.Pop().ToString(); // get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual, ExpectedValue);
        }

        [TestMethod]
        public void Bltint_not_intOnStack()
        {
            //Arrange                          
            Bltint Bltint_method = new Bltint();
            Bltint_method.VirtualMachine = VirtualMachine.Object;
            Bltint_method.VirtualMachine.Stack.Push("four");
            Bltint_method.Operands = new string[2] { "4", "%AddOne%" };

            //Act
            try
            {
                Bltint_method.Run(); //run instruction, should throw error
            }
            catch
            {
                return;
            }
            Assert.Fail();
        }

        //Bgrint tests --------------------------------
        [TestMethod]
        public void Bgrint_not_less()
        {
            //Arrange                          
            Bgrint Bgrint_method = new Bgrint();
            Bgrint_method.VirtualMachine = VirtualMachine.Object;
            string[] Operands = new string[2] { "6", "%AddOne%" };

            Bgrint_method.Operands = Operands;
            string ExpectedValue = "%AddOne%";

            //Act
            Bgrint_method.Run(); //run instruction 
            string actual = Bgrint_method.VirtualMachine.Stack.Pop().ToString(); // get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual, ExpectedValue);
        }

        [TestMethod]
        public void Bgrint_when_less()
        {
            //Arrange                          
            Bgrint Bgrint_method = new Bgrint();
            Bgrint_method.VirtualMachine = VirtualMachine.Object;

            Bgrint_method.Operands = new string[2] { "4", "%AddOne%" };            
            string ExpectedValue = VirtualMachine.Object.Stack.Peek().ToString();

            //Act
            Bgrint_method.Run(); //run instruction 
            string actual = Bgrint_method.VirtualMachine.Stack.Pop().ToString(); // get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual, ExpectedValue);
        }

        [TestMethod]
        public void Bgrint_not_intOnStack()
        {
            //Arrange                          
            Bgrint Bgrint_method = new Bgrint();
            Bgrint_method.VirtualMachine = VirtualMachine.Object;
            Bgrint_method.VirtualMachine.Stack.Push("four");
            Bgrint_method.Operands = new string[2] { "4", "%AddOne%" };

            //Act
            try
            {
                Bgrint_method.Run(); //run instruction, should throw error
            }
            catch
            {
                return;
            }
            Assert.Fail();
        }


        [TestCleanup]
        public void TestCleanup()
        {
            this.VirtualMachine = null;

        }
    }
}
