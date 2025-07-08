using System.Linq;
using CrowdedAddon;
using InnerNet;
using Lotus.Extensions;
using VentLib.Utilities.Extensions;

public static class GameStates
{
    public enum ServerType
    {
        Vanilla,
        Modded,
        Niko,
        Custom
    }

    public static bool InGame;
    public static bool AlreadyDied;
    public static bool IsModHost => PlayerControl.LocalPlayer.IsHost() || PlayerControl.AllPlayerControls.ToArray().Any(x => x.IsHost() /*&& x.IsModdedClient()*/);
    public static bool IsLobby => AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Joined;
    public static bool IsInGame => InGame;
    public static bool IsEnded => /*GameEndChecker.Ended ||*/ AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Ended;
    public static bool IsNotJoined => AmongUsClient.Instance.GameState == InnerNetClient.GameStates.NotJoined;
    public static bool IsOnlineGame => AmongUsClient.Instance.NetworkMode == NetworkModes.OnlineGame;
    public static bool IsLocalGame => AmongUsClient.Instance.NetworkMode == NetworkModes.LocalGame;
    public static bool IsFreePlay => AmongUsClient.Instance.NetworkMode == NetworkModes.FreePlay;
    public static bool IsInTask => InGame && !MeetingHud.Instance;
    public static bool IsMeeting => InGame && MeetingHud.Instance;
    public static bool IsVoting => IsMeeting && MeetingHud.Instance.state is MeetingHud.VoteStates.Voted or MeetingHud.VoteStates.NotVoted;

    public static bool IsCountDown => GameStartManager.InstanceExists && GameStartManager.Instance.startState == GameStartManager.StartingStates.Countdown;

    public static ServerType CurrentServerType
    {
        get
        {
            if (IsLocalGame && !IsNotJoined) return ServerType.Vanilla;

            string regionName = Utils.GetRegionName();

            return regionName switch
            {
                "Local Game" => ServerType.Custom,
                "EU" or "NA" or "AS" => ServerType.Vanilla,
                "MEU" or "MAS" or "MNA" => ServerType.Modded,
                _ => regionName.Contains("Niko", System.StringComparison.OrdinalIgnoreCase) ? ServerType.Niko : ServerType.Custom
            };
        }
    }

    /**********TOP ZOOM.cs***********/
    public static bool IsShip => ShipStatus.Instance != null;
    public static bool IsCanMove => PlayerControl.LocalPlayer != null && PlayerControl.LocalPlayer.CanMove;
    public static bool IsDead => PlayerControl.LocalPlayer != null && !PlayerControl.LocalPlayer.IsAlive();
}
