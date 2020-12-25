/********************************************************************
 * *
 * *
 * * Copyright (C) 2013-? BinSkin Corporation All rights reserved.
 * * 作者： BinGoo QQ：315567586 
 * * 请保留以上版权信息，否则作者将保留追究法律责任。
 * *
 * * 创建时间：2014-08-05
 * * 说明：
 * *
********************************************************************/
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace MapClient
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public class ObjectForScriptingHelper
    {
        Form mainWindow;

        public ObjectForScriptingHelper(Form main)
        {
            mainWindow = main;
        }

        //JS调WinForm方法的接口

        public void HtmlCmd(string cmd)
        {
            MessageBox.Show(cmd);
        }
    }



}
