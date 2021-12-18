using UdpKit;
using Photon.Bolt;
using System.Collections.Generic;

namespace BoltTokens
{
    public class RoomAccessToken : IProtocolToken
    {
        public string userName { get; set; }
        public string password { get; set; }

        public void Read(UdpPacket packet)
        {
            this.userName = packet.ReadString();
            this.password = packet.ReadString();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteString(this.userName);
            packet.WriteString(this.password);
        }
    }

    public class RoomMakeToken : IProtocolToken
    {
        public bool isLocked { get; set; }

        public void Read(UdpPacket packet)
        {
            this.isLocked = packet.ReadBool();
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteBool(this.isLocked);
        }
    }
    public class StartToken : IProtocolToken
    {
        public int playerAmount{get; set;}
        public List<bool> isBlueTeam { get; set; }
        public List<string> name { get; set; }

        public void Read(UdpPacket packet)
        {
            this.playerAmount = packet.ReadInt();
            for(int i = 0; i < this.playerAmount; i++){
                this.isBlueTeam[i] = packet.ReadBool();
                this.name[i] = packet.ReadString();
            }
        }

        public void Write(UdpPacket packet)
        {
            packet.WriteInt(this.playerAmount);
            for(int i = 0; i < this.playerAmount; i++){
                packet.WriteBool(this.isBlueTeam[i]);
                packet.WriteString(this.name[i]);
            }
        }
    }
}