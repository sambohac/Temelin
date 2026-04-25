
using System;
using UnityEngine;

public class Skill
{
    internal Stats stat1;
    internal int value1;

    internal Stats stat2;
    internal int value2;

    internal GameObject icon;
    internal GameObject textIcon;

    public Skill()
    {

    }

    public Skill(Stats stat1, int value1, Stats stat2, int value2)
    {
        this.stat1 = stat1;
        this.value1 = value1;
        this.stat2 = stat2;
        this.value2 = value2;
    }

    public override string ToString()
    {
        return $"{stat1} {value1}\n{stat2} {value2}";
    }

    public void Move(Vector3 newPos)
    {
        icon.transform.position = newPos;
        textIcon.transform.position = newPos;
    }

}
