# azino777

Версия android: от 9

Язык программирования: Unity, C#

Язык приложения: английский.

(!) На первом экране заглушки приложения разместить текст: I agree to the privacy policy and terms
of use. При нажатии на privacy policy and terms of use на новом экране открывается экран
приложения с текстом политики использования. При нажатии на кнопку назад - возвращение на
предыдущий экран, с которого перешли на текст.

1 Сцена. Webview - (Если у телефона нет доступа к интернету В РЕЖИМЕ САМОЛЕТА например
то открывается сцена 2 (ИГРА) если есть интернет, то 1 сцена должна открыть ссылку google.com

2 Сцена. При нажатии на кнопку Play - следующий экран. При нажатии на кнопку Exit - закрыть
приложение. Если пользователь не согласится с политикой - нажатием на «ползунок принятия», на
следующий экран он перейти не сможет.

3 Сцена. Симуляция игры. При нажатии на значок play происходит горизонтальное движение
верхнего и нижнего полей с элементами.
При совпадении двух элементов по вертикали ставка умножается на два. Стандартная ставка 10
очков. кнопки + и - неактивны.

ДИЗАЙН ПРИЛАГАЕТСЯ

1 Добавить в манифест случайные, не нужные для работы приложения разрешения.

2 Добавить разные, не относящиеся к приложению, но рабочие библиотеки в приложения
(“мусорный код“). Можно использовать только эти типы данных (библиотеки): Точное
местоположение, доступ к фото и видео, доступ к файлам и документам, сведения о приложении и
данные о его работе, идентификаторы устройства или другие идентификаторы.
Остальные данные добавлять нельзя, как ,например, такие: Личная информация, Финансовые
сведения, Здоровье и физическая активность, Сообщения, контакты и другие.

3 Делать разное количество layout файлов.

4 Приложение лучше всего писать с нуля, чтобы уменьшить количество повторяющегося кода.

(!) С кнопки назад можно вернуться на предыдущий экран в заглушке.

(!) Клавиатура при заполнении текстовых форм не закрывает её, а сдвигает наверх.

(!) Только вертикальное расположение экрана