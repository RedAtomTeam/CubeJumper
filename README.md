# CubeJumpre
Вдохновлённая Doodle Jump игра, где игрок управляет прыгающим кубиком, взбираясь вверх по случайно генерируемым платформам.

![Screenshot1](https://sun9-80.userapi.com/impg/KjRf8E5-qHWoV_g-xx3sVAMPJXIPPUGyI7c6vQ/-SG4OS1ityQ.jpg?size=1025x575&quality=95&sign=4139dee3d5344ac22683882e32ee6148&type=album)

## 🎮 Геймплей
Кубы постоянно появляются и падают сверху, а задача игрока уворачиваться и взбираться по ним, избегая падения в лаву. 

## Особенности:
- 🎮 Игровое пространство стабильно: блоки не исчезают, когда покидают область видимости
- ✨ Пиксельный стиль: визуальная простота помогает сфокусироваться на игровом процессе.
- ⬛ Физика геймплей: Прыгайте по блокам вверх, в стороны и скользите по стенам вверх и вниз
- 💡 Система улучшений: Собирайте монетки для улучшения прыжков, движений и скольжений.

![Screenshot2](https://sun9-61.userapi.com/impg/0GL9N3aKBrMFHEqbMK2LsoVC00I2NlI7NbMEcQ/cmw8YKs3rKU.jpg?size=1024x573&quality=95&sign=84d13b9882975bec989c5e4d192deb3d&type=album)
![Screenshot3](https://sun9-69.userapi.com/impg/35FTvdbr9Wjk8l4K2QJ_7ao8mbwfZbxXGb75yA/BqnfjWEOeTk.jpg?size=1023x575&quality=95&sign=3dbc3aac54c4e86135a63bc5362c4718&type=album)

## 🛠 Технические аспекты разработки
### Платформа
WebGL - игра размещена на веб-платформе Yandex Games и работает для десктопных устройств

### Стек
Движок: Unity

Packages: DOTween, YG Plugin(1.6)

### Вся работа приложения организована через систему скриптов:
- Спавн объектов:
  - ObjectSpawner - Для спавна объектов. Создаёт объекты SpawnedObject используя соответствующие пулы объектов, чтобы не создавать их через Instantiate.
  - SpawnedObject - Объект для спавна
  - SpawnedObjectPool - Пулл объектов для спавна
- Игрок:
  - InputHandler - Для обработки ввода
  - PlayerMovementSystem - Управление движением персонажа
  - ParticlesHandler - Управление частицами при движении персонажа.
  - PlayerLifeChecker - Управление здоровьем персонажа
- Баланс и улучшения:
  - BalanceManager - Управление балансом: обновление, добавление, списание, сохранение и загрузка
  - UpgradeSystem - Управление улучшениями: обновление прогресса, сохранение, загрузка

Также есть множество небольших классов для управления звуковыми эффектами, UI элементами и генерацией окружения(Например стен.)

![Screenshot4](https://sun9-58.userapi.com/impg/T9AUpm2zuELcDyVOuNPd2Q8bS6Pv277bQ5YBUw/M3ZiFg0NEYA.jpg?size=1025x573&quality=95&sign=4d0870402b7460eae1f91c9ba0e4e6c5&type=album)


## 💬 Контакты
- Почта: redatomteam@gmail.com
- ТГ: @gennady_a1
