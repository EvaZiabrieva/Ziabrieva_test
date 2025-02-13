public class FishView : BaseFishView
{
    public FishView(FishVisualsContainer container) : base(container) {}

    public override void SetWaterVisualsState(bool activeState)
    {
        if (activeState)
        {
            _container.ParticleSystem.Play();
        }
        else 
        {
            _container.ParticleSystem.Stop();
        }
    }
}
