using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardiologicalResearch
{
    public class Constants
    {
        public const string Yes = "да";
        public const string No = "нет";
        public const string Male = "мужской";
        public const string Female = "женский";

        public const string Point = ".";
        public const string Comma = ",";
        
        public const string Categorial = "Категориальный показатель";
        public const string Ordered = "Числовой показатель";
         
        public const string AgeDescription = "Количество полных лет";
        public const string GenderDescription = "Мужской или женский";
        public const string SmokeDescription = "Да или нет";
        public const string WaistDescription = "Объем талии в сантиметрах";
        public const string WeightDescription = "Масса тела в килограммах";
        public const string HeightDescription = "Длина тела в сантиметрах";
        public const string SBPraDescription = "Систолическое артериальное давление на правой руке в миллиметрах ртутного столба";
        public const string DBPraDescription = "Диастолическое артериальное давление на правой руке в миллиметрах ртутного столба";
        public const string SBPllDescription = "Систолическое артериальное давление на левой ноге в миллиметрах ртутного столба";
        public const string SBPrlDescription = "Систолическое артериальное давление на правой ноге в миллиметрах ртутного столба";
        public const string CfPWVDescription = "Скорость каротидно-феморальной пульсовой волны в метрах в секунду";
        public const string BaPWVDescription = "Скорость лодыжечно-плечевой пульсовой волны в метрах в секунду";
        public const string GlucoseDescription = "Уровень глюкозы в крови, микромолль на литр";
        public const string CholesterolDescription = "Уровень холестерина в крови, микромолль на литр";
        public const string PulseDescription = "Частота сердечных сокращений в ударах в секунду";
        public const string SCOREDescription = "Риск смертности от сердечно-сосудистых заболеваний по шкале SCORE";
        public const string StenocardiaDescription = "Наличие в анамнезе стенокардии, да или нет";
        public const string DiabetesDescription = "Наличие в анамнезе сахарного диабета, да или нет";
        public const string BMIDescription = "Индекс масы тела=вес/(рост2)";
        public const string CfPWV10Description = "Скорость каротидно-феморальной пульсовой волны, превышающая 10 см/c, да или нет";

        public const string AgeName = "Возраст";
        public const string GenderName = "Пол";
        public const string SmokeName = "Курильщик";
        public const string WaistName = "Объем талии";
        public const string WeightName = "Вес";
        public const string HeightName = "Рост";
        public const string SBPraName = "САДпр";
        public const string DBPraName = "ДАДпр";
        public const string SBPllName = "САДлн";
        public const string SBPrlName = "САДпн";
        public const string CfPWVName = "cfPWV";
        public const string BaPWVName = "baPWV";
        public const string GlucoseName = "Глюкоза";
        public const string CholesterolName = "Холестерин";
        public const string PulseName = "ЧСС";
        public const string SCOREName = "SCORE";
        public const string StenocardiaName = "Стенокардия";
        public const string DiabetesName = "Диабет";
        public const string BMIName = "ИМТ";
        public const string CfPWV10Name = "cfPWV>10";

        public const string IsNumber = "true";
        public const string IsNotNumber = "false";

        public const string Unknown = "Unknown";
        public const string Success = "Success";
        public const string Danger = "Danger";
        public const string Error = "Error";
        
        public const string AgePlaceholder = "лет";
        public const string WeightPlaceholder = "кг";
        public const string HeightPlaceholder = "см";
        public const string WaistPlaceholder = "см";
        public const string SBPraPlaceholder = "мм.рт.ст";
        public const string DBPraPlaceholder = "мм.рт.ст";
        public const string SBPllPlaceholder = "мм.рт.ст";
        public const string SBPrlPlaceholder = "мм.рт.ст";
        public const string PulsePlaceholder = "уд./мин";
        public const string CfPWVPlaceholder = "мм/с";
        public const string BaPWVPlaceholder = "мм/c";
        public const string GlucosePlaceholder = "ммоль/л";
        public const string CholesterolPlaceholder = "ммоль/л";
        public const string SCOREPlaceholder = "группа№";
        public const string IsCountable = "Значение является вычисляемым и не доступно для изменения пользователем.";

        public const string ArmsIndex15Name = "ArmsIndex15";
        public const string LegsIndex15Name = "LegsIndex15";
        public const string ABIName = "ABI";
        public const string SclerosisName = "Sclerosis";
    }
}