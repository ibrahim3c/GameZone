namespace GameZone.Models
{
    public class GameDevice
    {
        public int GameID {  get; set; }
        public Game Game { get; set; } = default!;

        public int DeviceID {  get; set; }
        public Device Device { get; set; } = default!;
    }
}
