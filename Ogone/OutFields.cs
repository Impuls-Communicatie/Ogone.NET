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
        AAVMAIL,
        AAVNAME,
        AAVPHONE,
        AAVZIP,
        ACCEPTANCE,
        ALIAS,
        AMOUNT,
        BIC,
        BIN,
        BRAND,
        CARDNO,
        CCCTY,
        CN,
        COLLECTOR_BIC,
        COLLECTOR_IBAN,
        COMPLUS,
        CREATION_STATUS,
        CREDITDEBIT,
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
        DEVICEID,
        DIGESTCARDNO,
        ECI,
        ED,
        EMAIL,
        ENCCARDNO,
        FXAMOUNT,
        FXCURRENCY,
        IP,
        IPCTY,
        MANDATEID,
        MOBILEMODE,
        NBREMAILUSAGE,
        NBRIPUSAGE,
        NBRIPUSAGE_ALLTX,
        NBRUSAGE,
        NCERROR,
        ORDERID,
        PAYID,
        PAYIDSUB,
        PAYMENT_REFERENCE,
        PM,
        SCO_CATEGORY,
        SCORING,
        SEQUENCETYPE,
        SIGNDATE,
        STATUS,
        SUBBRAND,
        SUBSCRIPTION_ID,
        TRXDATE,
        VC
    }
}
