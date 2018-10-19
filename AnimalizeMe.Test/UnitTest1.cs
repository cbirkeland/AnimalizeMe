using AnimalizeMe.Models;
using AnimalizeMe.Services;
using AnimalizeMe.Test.Mock;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AnimalizeMe.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void should_return_creature_if_just_one_in_database_and_it_have_correct_tag()
        {
            var mockEnv = new MockHostingEnvironment();

            var x = new AnimalService(mockEnv);

            var creatues = new List<Creature>
            {
                new Creature
                {
                    ImagePath="book.jpg",
                    CreatureTags = new List<CreatureTags>
                    {
                        new CreatureTags
                        {
                            Tag = new Tag
                            {
                                Name="book"
                            }
                        }
                    }
                }
            };

            string matchingAnimal = x.GetAnimalUrlThatMathcesTags(new []{ "book" }, creatues);

            Assert.AreEqual("book.jpg", matchingAnimal);
        }

        [TestMethod]
        public void should_return_null_if_no_tags_is_supplied()
        {
            var mockEnv = new MockHostingEnvironment();

            var x = new AnimalService(mockEnv);

            var creatues = new List<Creature>
            {
                new Creature
                {
                    ImagePath="book.jpg",
                    CreatureTags = new List<CreatureTags>
                    {
                        new CreatureTags
                        {
                            Tag = new Tag
                            {
                                Name="book"
                            }
                        }
                    }
                }
            };

            string matchingAnimal = x.GetAnimalUrlThatMathcesTags(new string[] { }, creatues);

            Assert.AreEqual(null, matchingAnimal);
        }
    }
}
