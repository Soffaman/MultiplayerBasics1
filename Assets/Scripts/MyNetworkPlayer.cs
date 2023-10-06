using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text _displayNameText = null;
    [SerializeField] private Renderer _displayRendererColour = null;

    [SyncVar(hook =nameof(HandleDisplayNameTextUpdated))]
    [SerializeField]
    private string _displayName = "Missing Name";

    [SyncVar(hook =nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color _displayColor = Color.white;

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        RpcLogNewName(newDisplayName);

        SetDisplayName(newDisplayName);
    }
    

    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        _displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColor(Color newDisplayColor)
    {
        _displayColor = newDisplayColor;
    }

    #endregion

    #region Client
    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        _displayRendererColour.material.SetColor("_BaseColor", newColor);
    }
    private void HandleDisplayNameTextUpdated(string oldName, string newName)
    {
        _displayNameText.text = newName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion
}
