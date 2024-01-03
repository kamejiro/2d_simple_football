using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// スクリプトA.cs
public class Calc_Deta : MonoBehaviour
{
    private int ap1, dp1, ap2, dp2;

    // プロパティ
    public int AP1
    {
        get { return ap1; }
        set { ap1 = value; }
    }

        public int DP1
    {
        get { return dp1; }
        set { dp1 = value; }
    }

        public int AP2
    {
        get { return ap2; }
        set { ap2 = value; }
    }

        public int DP2
    {
        get { return dp2; }
        set { dp2 = value; }
    }
}