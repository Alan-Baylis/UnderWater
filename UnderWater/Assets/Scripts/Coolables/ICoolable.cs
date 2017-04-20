namespace Coolables
{
    public interface ICoolable
    {
        int GetEnergyDelta(int deltaTemp);
        int GetMaxHeatTransfere();
        void ApplyHeatChange(int energy);
        int GetTemperature();
    }
}