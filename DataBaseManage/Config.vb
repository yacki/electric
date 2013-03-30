Public Class Config
#Region "私有成员变量"
    '配置文件路径
    Private sPath As String
    '配置文件对象（xml格式）
    Private xDoc As Xml.XmlDocument
#End Region

#Region "构造函数"
    '默认构造函数(取得当前目录下的app.config配置)
    Public Sub New()
        Dim assemblyFilePath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim assemblyDirPath As String = System.IO.Path.GetDirectoryName(assemblyFilePath)
        Dim configFilePath As String = assemblyDirPath & "\app.config"
        sPath = configFilePath
        xDoc = New Xml.XmlDocument
        xDoc.Load(sPath)
    End Sub

    '用户指定配置文件的构造函数
    Public Sub New(ByVal s_Path As String)
        sPath = s_Path
        xDoc = New Xml.XmlDocument
        xDoc.Load(sPath)
    End Sub
#End Region

#Region "公共接口"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' 读取属性
    ''' </summary>
    ''' <param name="keyName">属性名称</param>
    ''' <returns>属性值</returns>
    ''' -----------------------------------------------------------------------------
    Public Function GetValue(ByVal keyName As String) As String
        Dim str As String = ""
        Dim xElement As Xml.XmlElement = getElement(keyName)
        If Not xElement Is Nothing Then
            str = xElement.GetAttribute("connectionString")
        End If
        Return str
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' 设置属性
    ''' </summary>
    ''' <param name="keyName">属性名称</param>
    ''' <param name="keyValue">属性值</param>
    ''' <param name="bCreate">新增标志（为True的时候当属性不存在时则新增这个属性，默认为False）</param>
    ''' <returns>成功返回True，反之为False</returns>
    ''' -----------------------------------------------------------------------------
    Public Function SetValue(ByVal keyName As String, ByVal keyValue As String, Optional ByVal bCreate As Boolean = False) As Boolean
        Dim flag As Boolean = True
        Try
            Dim xElement As Xml.XmlElement = getElement(keyName)
            If Not xElement Is Nothing Then
                xElement.SetAttribute("value", keyValue)
            ElseIf bCreate Then
                Dim xNode As Xml.XmlNode = getXmlNode()
                xElement = xDoc.CreateElement("add")
                xElement.SetAttribute("key", keyName)
                xElement.SetAttribute("value", keyValue)
                xNode.AppendChild(xElement)
            End If
            xDoc.Save(sPath)
        Catch
            flag = False
        End Try
        Return flag
    End Function
#End Region

#Region "私有成员"
    '取得单个元素
    Private Function getElement(ByVal elemName As String) As Xml.XmlElement
        Dim xElement As Xml.XmlElement
        Try
            Dim node As Xml.XmlNode = getXmlNode()
            Dim s As String = "//add[@key='" & elemName & "']"
            xElement = CType(node.SelectSingleNode(("//add[@key='" & elemName & "']")), Xml.XmlElement)
        Catch
            xElement = Nothing
        End Try
        Return xElement
    End Function

    '取得节点
    Private Function getXmlNode() As Xml.XmlNode
        Return xDoc.SelectSingleNode("//connectionStrings")
    End Function
#End Region


End Class
