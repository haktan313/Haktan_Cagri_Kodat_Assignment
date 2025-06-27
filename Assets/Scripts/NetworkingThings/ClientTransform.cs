
using Unity.Netcode.Components;

public class ClientTransform : NetworkTransform
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}