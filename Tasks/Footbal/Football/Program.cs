using FluentAssertions;
using NUnit.Framework;

namespace Football {
    /*Как реализовать здачку в ООП? Есть задача: Две команды, А и Б, играют в футбол.
     Некто делает свою ставку на результат матча, например, 1:2. 
     По результату матча становится известен настоящий счёт, 
     и нам надо выдать тот или иной приз. Если некто угадал точный счёт, он получает большой приз. Если некто угадал исход матча 
     (выигрыш той или иной команды, или же ничью) - он получает маленький приз. Если же он не угадал, он получает нулевой приз. 
     Необходимо написать функцию которая принимает в качестве аргументов предполагаемый и реальный счёт, и возвращает целое число 0, 1 или 2 
     (нулевой, маленький или большой приз). – 
     материал взят с сайта Студворк https://studwork.org/qa/programmirovanie/1101391-kak-realizovat-zdachku-v-oop-est-zadacha-dve-komandy-a-i-b-igrayut-v-futbol-nekto-delaet-svoyu-stavku-na-rezultat*/
    
    public class GiftService {
        public const int BigGift = 2;
        public const int MiddleGift = 1;
        public const int NoGift = 0;
        
        public int GetGift(int first, int second, int expectedFirst, int expectedSecond) {
            if (first == expectedFirst && second == expectedSecond) {
                return BigGift;
            }
            if ((first > second && expectedFirst > expectedSecond)
                || (first < second && expectedFirst < expectedSecond)
                || (first == second && expectedFirst == expectedSecond)) {
                return MiddleGift;
            }
            return NoGift;
        }
    }

    [TestFixture]
    public class GiftServiceTests {
        private GiftService _giftService;

        [SetUp]
        public void SetUp() {
            _giftService = new GiftService();
        }

        [TestCase(5, 5, 5, 5, GiftService.BigGift)]
        [TestCase(4, 5, 3, 5, GiftService.MiddleGift)]
        [TestCase(6, 5, 7, 5, GiftService.MiddleGift)]
        [TestCase(5, 5, 6, 6, GiftService.MiddleGift)]
        [TestCase(6, 5, 3, 5, GiftService.NoGift)]
        [TestCase(4, 5, 7, 5, GiftService.NoGift)]
        [TestCase(5, 5, 7, 5, GiftService.NoGift)]
        [TestCase(6, 5, 5, 5, GiftService.NoGift)]
        [TestCase(4, 5, 5, 5, GiftService.NoGift)]
        public void ReturnsGiftByData(int first, int second, int expectedFirst, int expectedSecond, int expectedGift) {
            _giftService.GetGift(first, second, expectedFirst, expectedSecond).Should().Be(expectedGift);
        }
    }
}
