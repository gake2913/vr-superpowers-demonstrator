using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingFlower : Selectable
{

    private Flower flower;

    new void Start()
    {
        base.Start();

        flower = GetComponent<Flower>();
    }

    public override void Select(Selector selector)
    {
        base.Select(selector);

        flower.AddStage();
    }

}
