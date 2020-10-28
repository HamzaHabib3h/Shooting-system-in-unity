/// <summary>
/// A child class of Gun class
/// </summary>
public class AssaultRifle : Gun
{

    #region Fields

    static new string name = "AssaultRifle";

    #endregion

    #region Proporties

    public static string Name
    {
        get { return name; }
    }

    #endregion


    protected override void Start()
    {
        ammoType = AmmoType.Assualt;
        coolDownSeconds = 0.1f;
        range = 25f;
        power = 50;
        base.Start();
        
    }

    protected override void Fire(AmmoType ammoType, float power)
    {

        if (coolDownTimer.Finished)
        {
            coolDown = true;
        }
        if (coolDown)
        {
            CastRay();
        }
        
    }
}
