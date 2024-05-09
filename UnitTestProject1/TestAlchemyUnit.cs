using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AlchemyNS_;
using System.Diagnostics;

namespace TestAlchemyUnit
{
    /// <summary>
    /// Класс проверки класса Alchemy
    /// </summary>
    [TestClass]
    public class TestAlchemyUnit
    {
        /// <summary>
        /// Проверяет правильность функции setWeight а также конструктора
        /// </summary>
        [TestMethod]
        public void TestSet()
        {
            Alchemy sample1 = new Alchemy("Огонь", 10);
            Alchemy sample2 = new Alchemy("Вода", 5);
            Alchemy sample3 = new Alchemy("Земля", 20);

            sample1.setWeight(15);
            sample2.setWeight(8);
            sample3.setWeight(-5);


            Assert.AreEqual(sample1.getName() , "Огонь");
            Assert.AreEqual(sample1.getColor(), "Красный");
            Assert.AreEqual(sample2.getName(), "Вода");
            Assert.AreEqual(sample2.getColor(), "Синий");
            Assert.AreEqual(sample3.getName(), "Земля");
            Assert.AreEqual(sample3.getColor(), "Коричневый");

            Assert.AreEqual(sample1.getWeight(), 14);
            Assert.AreEqual(sample2.getWeight(), 8);
            Assert.AreEqual(sample3.getWeight(), 0);
        }
        /// <summary>
        /// Проверяет правильность работы смешивания
        /// </summary>
        [TestMethod]
        
        public void TestBlend()
        {
            Alchemy sample1 = new Alchemy("Огонь", 10);
            Alchemy sample2 = new Alchemy("Вода", 5);
            Alchemy sample3 = new Alchemy("Земля", 20);

            sample1.setWeight(15);
            sample2.setWeight(8);
            sample3.setWeight(-5);

            sample1.Blend(sample2);
            sample2.Blend(sample3);
            Assert.AreEqual(sample1.getWeight(), 16);
            Assert.AreEqual(sample2.getWeight(), 7);
            Assert.AreNotEqual(sample2.getColor(), "Синий");
        }
        /// <summary>
        /// Проверяет правильность работы очистки
        /// </summary>
        [TestMethod]
        public void TestTransferClear()
        {
            Alchemy sample1 = new Alchemy("Огонь", 10);
            Alchemy sample2 = new Alchemy("Вода", 5);

            sample1.transfer(sample2);
            Assert.AreEqual(sample1.getName() , "Вода");
            Assert.AreEqual(sample1.getWeight(), 5);
            Assert.AreEqual(sample1.getColor(), "Синий");

            sample2.clear();
            Assert.AreEqual(sample2.getName(), "Эфир");
            Assert.AreEqual(sample2.getWeight(), 0);
            Assert.AreEqual(sample2.getColor(), "Чёрный");
        }

    }
}
