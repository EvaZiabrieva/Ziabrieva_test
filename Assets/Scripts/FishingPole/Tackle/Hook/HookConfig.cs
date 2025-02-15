public class HookConfig : BaseConfig
{
    public int baitCapacity = 1;
    public float failChance = 0;

    public HookConfig(string id, int baitCapacity, float failChance) : base(id)
    {
        this.baitCapacity = baitCapacity;
        this.failChance = failChance;
    }
}
