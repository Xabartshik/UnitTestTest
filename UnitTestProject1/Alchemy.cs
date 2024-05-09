using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Linq;

namespace AlchemyNS_
{
    /// <summary>
    /// Класс Алхимия. Реализует взаимодействие и смешивание элементов первого порядка (4)
    /// Позволяет смешивать материалы с соответствующим изменением веса и цвета материала
    /// </summary>
    public class Alchemy
    {

        private string name; // Имя элемента
        private string color; // Цвет элемента
        private double weight; // Вес элемента

        /// <summary>
        /// Метод для установки веса элемента. Если вес меньше 0, то он становится = 0
        /// </summary>
        /// <param name="newWeight"> Вес элемента double. Не может быть меньше 0 </param>
        public void setWeight(double newWeight)
        {
            this.weight = (newWeight >= 0) ? newWeight : 0;
        }

        /// <summary>
        /// Метод для получения веса элемента
        /// </summary>
        /// <returns> Возвращает вес элемента </returns>
        public double getWeight()
        {
            return this.weight;
        }

        /// <summary>
        /// Возвращает название элемента
        /// </summary>
        /// <returns> Возвращает название элемента </returns>
        public string getName()
        {
            return this.name;
        }

        /// <summary>
        /// Устанавливает название элемента
        /// </summary>
        /// <param name="newName"> Строка </param>
        public void setName(string newName)
        {
            this.name = newName;
        }

        /// <summary>
        /// Возвращает цвет элемента
        /// </summary>
        /// <returns> Возвращает цвет элемента </returns>
        public string getColor()
        {
            return this.color;
        }

        /// <summary>
        /// Устанавливает цвет элемента
        /// </summary>
        /// <param name="newColor"> Строка </param>
        public void setColor(string newColor)
        {
            this.color = newColor;
        }
        /// <summary>
        /// Перенос значений одного элемента другому с очисткой
        /// </summary>
        /// <param name="Element"> Alchemy Element, у которого будет взяты значения </param>
        public void transfer(Alchemy Element)
        {
            setName(Element.getName()); 
            setColor(Element.getColor());
            setWeight(Element.getWeight()); 
            Element.setName("Эфир");
            Element.setColor("Чёрный");   
            Element.setWeight(0);
        }
        /// <summary>
        /// Очистка значений элемента (сброс на значения по умолчанию)
        /// </summary>
        public void clear()
        {
            this.setName("Эфир");
            this.setColor("Чёрный");
            this.setWeight(0);
        }

        /// <summary>
        /// Создает элемент одного из пяти типов с заранее предопределенными названиями
        /// </summary>
        /// <param name="newName"> Название элемента </param>
        /// <param name="newWeight"> Вес элемента </param>
        //Конструктор
        public Alchemy(string newName, double newWeight)
        {

            switch (newName)
            {
                case ("Огонь"):
                    this.name = newName;
                    this.color = "Красный";
                    break;
                case ("Вода"):
                    this.name = newName;
                    this.color = "Синий";
                    break;

                case ("Земля"):
                    this.name = newName;
                    this.color = "Коричневый";
                    break;
                case ("Воздух"):
                    this.name = newName;
                    this.color = "Зеленый";
                    break;
                default:
                    this.name = "Эфир";
                    this.color = "Черный";
                    break;
            }
            this.setWeight(newWeight);
        }
        /// <summary>
        /// Смешивает два элемента между собой согласно следующей таблице: <br />
        ///        Огонь   Вода   Земля Воздух <br />
        /// Огонь |Плазма|Спирт |Лава  |Газ   |<br />
        /// Вода  |Спирт |Лёд   |Грязь |Дождь |<br />
        /// Земля |Лава  |Грязь |Металл|Песок |<br />
        /// Воздух|Газ   |Дождь |Песок |Вакуум|<br />
        /// Вещества реагируют по весу 1:1, остатки вещества после реакции остаются в Element,
        /// результат -- в this
        /// </summary>
        /// <param name="Element"> Элемент, указанный для смешивания</param>
        public void Blend(Alchemy Element)
        {
            string buferName, buferColor;
            double buferWeight;
            string[,] mixCheck = new string[,]
            {
                {"Плазма", "Спирт", "Лава", "Газ"},
                {"Спирт", "Лёд", "Грязь", "Дождь"},
                {"Лава", "Грязь", "Металл", "Песок"},
                {"Газ", "Дождь", "Песок", "Вакуум"}
            };


            Dictionary<string, int> elementID = new Dictionary<string, int>()
            {
                {"Огонь", 0},
                {"Вода", 1},
                {"Земля", 2},
                {"Воздух", 3},
            };
            //Проверка на совпадение смешивания и смешивание
            //Изменение веса исходя из ситуации
            if ((this.getWeight() == 0) || (Element.getWeight() == 0))
            {
                if (this.getWeight() == 0)
                {
                    this.transfer(Element);
                }
                else
                {
                    Element.clear();
                }
            }
            else
            {
                if (this.weight > Element.getWeight())
                {
                    buferName = this.getName();
                    buferColor = this.getColor();
                    buferWeight = this.getWeight() - Element.getWeight();
                    this.weight = 2 * Element.getWeight();
                    this.setName(Element.getName());
                    Element.setWeight(buferWeight);
                    Element.setName(buferName);
                    Element.setColor(buferColor);

                }
                else
                {
                    Element.setWeight(Element.getWeight() - this.weight);
                    this.weight = 2 * this.getWeight();
                }
                //Если реагирует Эфир, то все становится Эфиром
                if ((this.name == "Эфир") || (Element.name == "Эфир"))
                {
                    this.name = "Эфир";
                    this.color = "Черный";
                }
                else
                {
                    this.setName(mixCheck[elementID[this.name], elementID[Element.name]]);
                    Random random = new Random();
                    int randomNumber = random.Next(0, 2);
                    // Использование случайного выбора для выбора цвета нового элемента
                    this.color = (randomNumber == 0) ? this.color : Element.color;
                }
            }
        }
    }
    /// <summary>
    /// Класс тестирования Alchemy
    /// </summary>
    public class Tests
    {
        /// <summary>
        /// Функция тестирования всех указанных здесь функций
        /// </summary>
        public void test()
        {
            Alchemy sample1 = new Alchemy("Огонь", 10);
            Alchemy sample2 = new Alchemy("Вода", 5);
            Alchemy sample3 = new Alchemy("Земля", 20);

            // Проверка установки веса
            sample1.setWeight(15);
            sample2.setWeight(8);
            sample3.setWeight(-5);
            Debug.Assert(sample1.getWeight() == 15);
            Debug.Assert(sample2.getWeight() == 8);
            Debug.Assert(sample3.getWeight() == 0);

            //Проверка установки цвета и имени элемента
            Debug.Assert(sample1.getName() == "Огонь");
            Debug.Assert(sample1.getColor() == "Красный");
            Debug.Assert(sample2.getName() == "Вода");
            Debug.Assert(sample2.getColor() == "Синий");
            Debug.Assert(sample3.getName() == "Земля");
            Debug.Assert(sample3.getColor() == "Коричневый");

            // Проверка смешивания
            sample1.Blend(sample2);
            sample2.Blend(sample3);
            Debug.Assert(sample1.getWeight() == 16);
            Debug.Assert(sample2.getWeight() == 7);
            Debug.Assert(sample2.getColor() != "Синий");

            // Проверка передачи и очистки

            sample1.transfer(sample2);
            Debug.Assert(sample1.getName() == "Огонь");
            Debug.Assert(sample1.getWeight() == 7);
            Debug.Assert(sample1.getColor() == "Красный");

            sample2.clear();
            Debug.Assert(sample2.getName() == "Эфир");
            Debug.Assert(sample2.getWeight() == 0);
            Debug.Assert(sample2.getColor() == "Чёрный");

        }
    }
}