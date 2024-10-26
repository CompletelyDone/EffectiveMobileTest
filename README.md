# EffectiveMobile

Необходимо разработать консольное приложение (по желанию можно WinForms
либо WebApi) для службы доставки, которое фильтрует заказы в зависимости от
количества обращений в конкретном районе города и времени обращения с и по.<br />
Входные данные для каждой строки содержат следующую информацию: <br />
● Номер заказа - можно использовать уникальный идентификатор или
придумать свой; <br />
● Вес заказа в килограммах; <br />
● Район заказа, можно придумать либо название либо идентификатор
района; <br />
● Время доставки заказа - в формате: yyyy-MM-dd HH:mm:ss. <br />
Для исходных данных можно использовать либо файл с данными
(рекомендуется) либо любые СУБД, которые можно легко установить для проверки.
Также необходимо сделать логирование основных операций, а также
валидацию входных данных. <br />

В результирующий файл либо БД необходимо вывести результат фильтрации
заказов для доставки в конкретный район города в ближайшие полчаса после времени
первого заказа.
Данные для фильтрации (можно передавать через параметры консольного
приложения _cityDistrict, _firstDeliveryDateTime): <br />
● Район доставки; <br />
● Время первой доставки. <br />
Выходные файлы: <br />
● логирование - в случае консольного приложения определить задание адреса
файла через командную строку: _deliveryLog - путь к файлу с логами, либо
создать в СУБД таблицу с логами; <br />
● результат - записывать по адресу: _deliveryOrder - путь к файлу с результатом
выборки либо в СУБД таблицу. <br />
По возможности, кроме передачи параметров через командную строку либо
форму, можно реализовать частичную либо полную передачу параметров через файлы
конфигурации или переменные среды <br />
Программа не должна ломаться от некорректных входных данных (реализовать
в валидации), ошибок ввода-вывода и прочим причинам, которые можно
предусмотреть (реализовать обработку возможных ошибок). <br />
Решение должно быть предоставлено в виде исходных кодов в архиве, при
необходимости с дампом базы данных, в виде архива или ссылки на репозиторий с
решением. <br />
Код должен быть оптимальным, читаемым для разработчика и удобным для
пользователя, при разработке желательно покрытие несколькими тестами и
использование общераспространенных практик (паттерны проектирования, KISS...)
Результат тестового задания должен содержать текстовый файл readme.txt с
инструкцией по настройке и конфигурированию приложения (если необходимо).
