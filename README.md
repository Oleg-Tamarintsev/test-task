# test-task
Напишите функцию, которая реализует команду смены директории (cd) для абстрактной файловой системы.

Примечания:

1. Корневой путь '/'.

2. Разделитель пути '/'.

3. Родительская директория адресуется через "..".

4. Имена директорий состоят только из английского алфавита (A-Z и a-z)

5. Функция не допускает недействительные пути

6. Не использовать встроенные функции работы с путями.

7. Обязательно использовать Unit-тесты.

8. Не использовать регулярные выражения (Regex)

Например:

Path path = new Path("/a/b/c/d");

path.Cd("../x");

Console.WriteLine(path.CurrentPath);

должно отобразить '/a/b/c/x'.
