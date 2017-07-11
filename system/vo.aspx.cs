using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Redsz;
using Redsz.DAO;
using Redsz.BO;
using Redsz.VO;
using System.Collections.Generic;
using System.Reflection;

public partial class system_valueObject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //check.checkAdimnLogin(Request, Response);
    }

    protected void click_add(object sender, EventArgs e)
    {
        UtilBO.createValueObject(Request.Form["desc"], Request.Form["tablename"], Request.Form["filename"]);
    }
}
