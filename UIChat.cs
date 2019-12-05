using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIChat : MonoBehaviour
{
  private const int CHAT_WIDTH  = 600;
  private const int CHAT_HEIGHT = 475;
  private const int USER_WIDTH  = 80;


  private string _Chat;
  private List<string> _Talk;
  private Dictionary<int, string> _User;

  protected void Start()
  {
    _Chat = "";

    _Talk = new List<string>();
    _User = new Dictionary<int,string>();
  }

  public void AddChat(string user, string message)
  {
    _Talk.Insert(0, user + ": " + message);

    if (_Talk.Count > 25) _Talk.RemoveAt(25);
  }

  public void AddUser(int uid, string user)
  {
    if (!_User.ContainsKey(uid))
    {
      _User[uid] = user;
    }
  }

  public void RemoveUser(int uid)
  {
    if (_User.ContainsKey(uid))
    {
      _User.Remove(uid);
    }
  }

  protected void Update()
  {
  }

  protected void OnGUI()
  {
    GUI.BeginGroup(new Rect((Screen.width - CHAT_WIDTH) / 2 - USER_WIDTH, (Screen.height - CHAT_HEIGHT) / 2, CHAT_WIDTH, CHAT_HEIGHT));
    GUI.Box(new Rect(0, 0, CHAT_WIDTH, CHAT_HEIGHT), "");

    for (int i = 0; i < 25; i++)
    {
      if (i >= _Talk.Count) break;

      GUI.Label(new Rect(2, 18 * (24 - i) - 2, CHAT_WIDTH - 4, 22), _Talk[i]);
    }

    _Chat = GUI.TextField(new Rect(0, CHAT_HEIGHT - 25, CHAT_WIDTH - 60, 25), _Chat, 300);
    if (GUI.Button(new Rect(CHAT_WIDTH - 60, CHAT_HEIGHT - 25, 60, 25), "ส่ง"))
    {
      Packet packet = Game.GetInstance().GetRemote().GetPacket();
      packet.SendChat(_Chat);

      _Chat = "";
    }
    GUI.EndGroup();

    GUI.BeginGroup(new Rect((Screen.width - CHAT_WIDTH) / 2 + CHAT_WIDTH - USER_WIDTH, (Screen.height - CHAT_HEIGHT) / 2, USER_WIDTH, CHAT_HEIGHT));
    GUI.Box(new Rect(0, 0, USER_WIDTH, CHAT_HEIGHT), "");

    int idx = 0;
    foreach (var v in _User)
    {
      if (idx >= 25) break;

      GUI.Label(new Rect(2, 18 * idx - 2, USER_WIDTH - 4, 22), v.Value+" [" + v.Key.ToString() + "]");
      idx = idx + 1;
    }
    GUI.EndGroup();
  }
}
// 
// 