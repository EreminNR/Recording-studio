using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Recording_studio;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ListViewTest()
        {
            Services услуги = new Services();
            int actual = услуги.list.Items.Count;
            int expected = 4;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddTest()
        {
            admin2 услуги = new admin2();
            byte[] image;
            string path = "C:\\Users\\Nikit\\OneDrive\\Изображения\\абв.jpg";
            image = System.IO.File.ReadAllBytes(path);
            string name = "Тест";
            string description = "Тест";          
            int price = 5000;
            услуги.AddServices(name, description, image, price);
            Recording_studio.StudioEntities context = new Recording_studio.StudioEntities();
            var услуга = context.Услуги.OrderByDescending(x => x.IDУслуги).First();
            Assert.AreEqual(name, услуга.Название);
            Assert.AreEqual(description, услуга.Описание);
            CollectionAssert.AreEqual(image, услуга.Изображение);
            Assert.AreEqual(price, услуга.Цена);
            

        }
        [TestMethod]
        public void EditTest()
        {
            int id = 7;

            string name = "Тест";
            string description = "Тест";           
            int price = 6000;
            byte[] image;
            string path = "C:\\Users\\Nikit\\OneDrive\\Изображения\\ава.jpg";
            image = System.IO.File.ReadAllBytes(path);
            string name1;
            string description1;
            int price1;
            byte[] image1;

            Recording_studio.StudioEntities context = new Recording_studio.StudioEntities();
            Recording_studio.Услуги услуги;
            услуги = context.Услуги.Where(x => x.IDУслуги == id).FirstOrDefault();
            //сохраняю старые значения
            name1 = услуги.Название;
            description1 = услуги.Описание;
            price1 = Convert.ToInt16(услуги.Цена);
            image1 = услуги.Изображение;      
            admin2.EditServices(id, name, description, image, price);
            Recording_studio.StudioEntities context2 = new Recording_studio.StudioEntities();
            Recording_studio.Услуги ServicesAfterEdit;
            ServicesAfterEdit = context2.Услуги.Where(x => x.IDУслуги == id).FirstOrDefault();
            Assert.AreNotEqual(name1, ServicesAfterEdit.Название);
            Assert.AreNotEqual(description1, ServicesAfterEdit.Описание);
            Assert.AreNotEqual(price1, ServicesAfterEdit.Цена);
            CollectionAssert.AreNotEqual(image1, ServicesAfterEdit.Изображение);
        }
        [TestMethod]
        public void DeleteTest()
        {
            int id = 27;
            Recording_studio.Услуги actual;
            Recording_studio.StudioEntities context = new Recording_studio.StudioEntities();
            id = (from x in context.Услуги select x.IDУслуги).ToList().Last();
            admin2.DeleteServices(id);
            actual = context.Услуги.Where(x => x.IDУслуги == id).FirstOrDefault();
            Assert.IsNull(actual);
        }
    }
}
