# Unity Game Analytics Insecure HTTP Option Fix

Этот репозиторий содержит скрипты, которые решают проблему с ошибкой `Insecure connection not allowed` при использовании плагина GameAnalytics в Unity.

## Проблема

При использовании плагина GameAnalytics в Unity может возникнуть следующая ошибка:

```
Unity GA | Job failed with exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.InvalidOperationException: Insecure connection not allowed
```

Полный стектрейс:
```
Unity GA | Job failed with exception: System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation. ---> System.InvalidOperationException: Insecure connection not allowed
at (wrapper managed-to-native) UnityEngine.Networking.UnityWebRequest.BeginWebRequest(UnityEngine.Networking.UnityWebRequest)
  at UnityEngine.Networking.UnityWebRequest.SendWebRequest () [0x00001] in /Users/bokken/build/output/unity/unity/Modules/UnityWebRequest/Public/UnityWebRequest.bindings.cs:289 
  at (wrapper managed-to-native) System.Reflection.RuntimeMethodInfo.InternalInvoke(System.Reflection.RuntimeMethodInfo,object,object[],System.Exception&)
  at System.Reflection.RuntimeMethodInfo.Invoke (System.Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x0006a] in <f06813fea36f4d329c559563c08ea387>:0 
   --- End of inner exception stack trace ---
  at System.Reflection.RuntimeMethodInfo.Invoke (System.Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x00083] in <f06813fea36f4d329c559563c08ea387>:0 
  at System.Reflection.MethodBase.Invoke (System.Object obj, System.Object[] parameters) [0x00000] in <f06813fea36f4d329c559563c08ea387>:0 
  at Google.PortableWebRequest.StartRequest (Google.PortableWebRequest+HttpMethod method, System.String url, System.Collections.Generic.IDictionary`2[TKey,TValue] headers, UnityEngine.WWWForm form) [0x0019e] in /Users/chkuang/Workspace/git/unity-jar-resolver/source/VersionHandlerImpl/src/PortableWebRequest.cs:508 
  at Google.PortableWebRequest+<StartRequestOnMainThread>c__AnonStorey6.<>m__C () [0x00000] in /Users/chkuang/Workspace/git/unity-jar-resolver/source/VersionHandlerImpl/src/PortableWebRequest.cs:458 
  at Google.RunOnMainThread.ExecuteNext () [0x0003d] in /Users/chkuang/Workspace/git/unity-jar-resolver/source/VersionHandlerImpl/src/RunOnMainThread.cs:486 
UnityEngine.Debug:LogError (object)
Google.RunOnMainThread:ExecuteNext () (at /Users/chkuang/Workspace/git/unity-jar-resolver/source/VersionHandlerImpl/src/RunOnMainThread.cs:488)
Google.RunOnMainThread:<ExecuteAllUnnested>m__12 () (at /Users/chkuang/Workspace/git/unity-jar-resolver/source/VersionHandlerImpl/src/RunOnMainThread.cs:536)
Google.RunOnMainThread:RunAction (System.Action) (at /Users/chkuang/Workspace/git/unity-jar-resolver/source/VersionHandlerImpl/src/RunOnMainThread.cs:343)
Google.RunOnMainThread:ExecuteAllUnnested (bool) (at /Users/chkuang/Workspace/git/unity-jar-resolver/source/VersionHandlerImpl/src/RunOnMainThread.cs:530)
Google.RunOnMainThread:ExecuteAll () (at /Users/chkuang/Workspace/git/unity-jar-resolver/source/VersionHandlerImpl/src/RunOnMainThread.cs:512)
UnityEditor.EditorApplication:Internal_CallUpdateFunctions () (at /Users/bokken/build/output/unity/unity/Editor/Mono/EditorApplication.cs:381)
```

Эта ошибка возникает из-за настройки `insecureHttpOption` в файле `ProjectSettings.asset`. При значении `0` (по умолчанию) Unity не позволяет использовать небезопасные HTTP-соединения, что вызывает эту ошибку.

## Решение

Для устранения этой ошибки необходимо изменить значение `insecureHttpOption` на `1` в файле `ProjectSettings.asset`. В данном репозитории представлены два скрипта, которые помогут автоматизировать этот процесс:

- **InsecureHttpOptionEditor**: Позволяет вручную изменить настройку через меню в Unity Editor.
- **InsecureHttpOptionBuildHandler**: Автоматически изменяет настройку перед сборкой и восстанавливает её после завершения сборки.

## Установка

### Установка через Unity Package Manager

1. Откройте `Packages/manifest.json`.
2. Добавьте следующую строку в раздел `dependencies`:

```json
"com.rimurudev.unity-ga-insecurehttpoption": "https://github.com/RimuruDev/Unity-GA-InsecureHttpOption.git"
```

3. Или установите через Package Manager `"https://github.com/RimuruDev/Unity-GA-InsecureHttpOption.git"`
4. Сохраните файл и откройте Unity. Пакет будет автоматически установлен.

### Установка из релиза

1. Скачайте последний релиз из [Releases](https://github.com/RimuruDev/Unity-GA-InsecureHttpOption/releases).
2. Распакуйте содержимое архива в папку `Assets/Plugins/RimuruDev/Unity-GA-InsecureHttpOption` вашего проекта.

## Использование

### В редакторе Unity

Для ручного изменения настройки `insecureHttpOption` используйте меню в Unity:

- **Set insecureHttpOption to 1**: `RimuruDev Tools/GA/Set insecureHttpOption to 1`
- **Set insecureHttpOption to 0**: `RimuruDev Tools/GA/Set insecureHttpOption to 0`

### Автоматическая обработка при сборке

Скрипт `InsecureHttpOptionBuildHandler` автоматически изменит значение `insecureHttpOption` на `1` перед сборкой и восстановит его на `0` после завершения сборки.

## Вклад

Ваш вклад приветствуется! Если у вас есть предложения или исправления, создайте pull request или откройте issue.

## Лицензия

Этот проект лицензирован под MIT License. Подробности см. в файле [LICENSE](LICENSE).

---

Автор: RimuruDev
