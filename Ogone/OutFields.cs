using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ogone
{
    /// <summary>
    /// Allowed fields to return in the answer from Ogone
    /// </summary>
    public enum OutFields
    {
        AAVADDRESS,
        AAVCHECK,
        AAVZIP,
        ACCEPTANCE,
        ALIAS,
        AMOUNT,
        BIN,
        BRAND,
        CARDNO,
        CCCTY,
        CN,
        COMPLUS,
        CREATION_STATUS,
        CURRENCY,
        CVCCHECK,
        DCC_COMMPERCENTAGE,
        DCC_CONVAMOUNT,
        DCC_CONVCCY,
        DCC_EXCHRATE,
        DCC_EXCHRATESOURCE,
        DCC_EXCHRATETS,
        DCC_INDICATOR,
        DCC_MARGINPERCENTAGE,
        DCC_VALIDHOURS,
        DIGESTCARDNO,
        ECI,
        ED,
        ENCCARDNO,
        IP,
        IPCTY,
        NBREMAILUSAGE,
        NBRIPUSAGE,
        NBRIPUSAGE_ALLTX,
        NBRUSAGE,
        NCERROR,
        ORDERID,
        PAYID,
        PM,
        SCO_CATEGORY,
        SCORING,
        STATUS,
        SUBSCRIPTION_ID,
        TRXDATE,
        VC
    }
}
