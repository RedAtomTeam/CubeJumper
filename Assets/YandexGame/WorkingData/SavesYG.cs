
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1;                       // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        // ...

        public int currentSpeedLevel = -1;
        public int currentJumpLevel = -1;
        public int currentSlideLevel = -1;

        public int balance = 0;

        public int maxHeight = 0;
        public int maxTimeInSeconds = 0;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            currentSpeedLevel = -1;
             currentJumpLevel = -1;
             currentSlideLevel = -1;

             balance = 0;

             maxHeight = 0;
             maxTimeInSeconds = 0;
        // Допустим, задать значения по умолчанию для отдельных элементов массива

            openLevels[1] = true;
        }
    }
}
