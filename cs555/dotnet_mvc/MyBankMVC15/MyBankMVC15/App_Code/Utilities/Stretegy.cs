using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Stretegy
/// </summary>
public class Stretegy
{
    public bool passOrNot { get; set; }
    public String msg { get; set; }
    public Stretegy()
    {
        passOrNot = false;
        msg = "You cannot get a loan";
    }

    public static Stretegy testLoan(double chkAccBalance, double savAccBalance, double loanamt)
    {
        Stretegy stretegy = new Stretegy();
        if (chkAccBalance + savAccBalance < loanamt * 0.2)
        {
            stretegy.passOrNot = false;
            stretegy.msg = "The loan amount you are requering is too high.";
        }
        else
        {
            stretegy.passOrNot = true;
            stretegy.msg = "You loan is passed.";
        }
        return stretegy;
    }
}