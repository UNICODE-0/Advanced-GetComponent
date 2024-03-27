# Advanced-GetComponent
Сustom GetComponent for the unity engine, which is faster than the built-in one by more than 10 times!

**HOW TO USE**
1. Inherit your scripts from SMonoBehaviour.
2. Set the **Root** tag on the GameObject that will be associated with its child SMonoBehaviours.
3. Get the component using the Instance id of the required **Root** GameObject.

Examples:
```csharp
SMonoBehaviour instance = GetSharedComponent<SMonoBehaviour>(RootGameObject.GetInstanceID());
if(TryGetSharedComponent(RootGameObject.GetInstanceID(), out SMonoBehaviour sMonoBehaviour))
{
    // Some code
}
```
if you need to get a component not from SMonoBehaviour, then you must directly call the GetSharedComponent method
```csharp
SMonoBehaviour instance = SMonoBehaviour.GetSharedComponent<SMonoBehaviour>(RootGameObject.GetInstanceID());
```
