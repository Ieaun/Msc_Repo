using System;
using Moq;
using SVM;
using SVM.SimpleMachineLanguage;
using SVM.VirtualMachine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace SVMUnitTests
{
    [TestClass]
    public class IncrUnitTests
    {
        Mock<IVirtualMachine> VirtualMachine;

        //Incr tests-----------------------------------
        [TestInitialize]       
        public void TestInit()
        {
            this.VirtualMachine = new Mock<IVirtualMachine>();
            VirtualMachine.SetupAllProperties();

            Stack Stacker = new Stack();
            VirtualMachine.Object.Stack = Stacker;
        }

        [TestMethod]
        public void CanIncrement_Test()
        {
            //Arrange                
            int StackValue = -3;  //number to incr           
            VirtualMachine.Object.Stack.Push(StackValue);
          
            Incr Incr_method = new Incr();
            Incr_method.VirtualMachine = VirtualMachine.Object;

            int ExpectedValue = -2;

            //Act
            Incr_method.Run(); //run instruction 
            int actual = (int)VirtualMachine.Object.Stack.Pop(); // get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual,ExpectedValue);
        }
   


        [TestMethod]
        //call incr when the stack is empty
        public void Incr_When_Stack_Null()
        {
            //Arrange                          
            VirtualMachine.Object.Stack = null;
            Incr Incr_method = new Incr();
            Incr_method.VirtualMachine = VirtualMachine.Object;

            //Act
            try
            {
                Incr_method.Run(); //run instruction 
            }
            catch
            {
                //Assert (Verifiy true or false)
                return;
            }
            Assert.Fail();
        }


        [TestMethod]
        //test to try and pop result off stack after incr to make sure its putting it on stack
        public void Incr_Stack_Empty_Post_Incr()
        {
            //Arrange                                         
            int StackValue = -3;  //number to incr           
            VirtualMachine.Object.Stack.Push(StackValue);
            Incr Incr_method = new Incr();
            Incr_method.VirtualMachine = VirtualMachine.Object;
            Incr_method.Run(); //run instruction 

            //Act
            try
            {
                int actual = (int)VirtualMachine.Object.Stack.Pop();
                return;
            }
            catch
            {
                              
            }
            //Assert (Verifiy true or false)
            Assert.Fail();
        }


        //Decr tests-----------------------------------
        [TestMethod]
        // normal operation
        public void CanDecrement_Test()
        {
            //Arrange                
            int StackValue = 0;  //number to incr           
            VirtualMachine.Object.Stack.Push(StackValue);

            Decr Decr_method = new Decr();
            Decr_method.VirtualMachine = VirtualMachine.Object;

            int ExpectedValue = -1;

            //Act
            Decr_method.Run(); //run instruction 
            int actual = (int)VirtualMachine.Object.Stack.Pop(); // get result off stack

            //Assert (Verifiy true or false)
            Assert.AreEqual(actual, ExpectedValue);
        }

        [TestMethod]
        //call Decr when the stack is empty
        public void Decr_When_Stack_Null()
        {
            //Arrange                          
            VirtualMachine.Object.Stack = null;
            Decr Decr_method = new Decr();
            Decr_method.VirtualMachine = VirtualMachine.Object;

            //Act
            try
            {
                Decr_method.Run(); //run instruction 
            }
            catch
            {
                //Assert (Verifiy true or false)
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        //test to try and pop result off stack after Decr to make sure its putting it on stack
        public void Decr_Stack_Empty_Post_Incr()
        {
            //Arrange                                         
            int StackValue = -3;  //number to incr           
            VirtualMachine.Object.Stack.Push(StackValue);
            Decr Decr_method = new Decr();
            Decr_method.VirtualMachine = VirtualMachine.Object;
            Decr_method.Run(); //run instruction 

            //Act
            try
            {
                int actual = (int)VirtualMachine.Object.Stack.Pop();
                return;
            }
            catch
            {

            }
            //Assert (Verifiy true or false)
            Assert.Fail();
        }


        [TestCleanup]
        public void TestCleanup()
        {
            this.VirtualMachine = null;
        }
    }
}
