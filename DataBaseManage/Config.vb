Public Class Config
#Region "˽�г�Ա����"
    '�����ļ�·��
    Private sPath As String
    '�����ļ�����xml��ʽ��
    Private xDoc As Xml.XmlDocument
#End Region

#Region "���캯��"
    'Ĭ�Ϲ��캯��(ȡ�õ�ǰĿ¼�µ�app.config����)
    Public Sub New()
        Dim assemblyFilePath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Dim assemblyDirPath As String = System.IO.Path.GetDirectoryName(assemblyFilePath)
        Dim configFilePath As String = assemblyDirPath & "\app.config"
        sPath = configFilePath
        xDoc = New Xml.XmlDocument
        xDoc.Load(sPath)
    End Sub

    '�û�ָ�������ļ��Ĺ��캯��
    Public Sub New(ByVal s_Path As String)
        sPath = s_Path
        xDoc = New Xml.XmlDocument
        xDoc.Load(sPath)
    End Sub
#End Region

#Region "�����ӿ�"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' ��ȡ����
    ''' </summary>
    ''' <param name="keyName">��������</param>
    ''' <returns>����ֵ</returns>
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
    ''' ��������
    ''' </summary>
    ''' <param name="keyName">��������</param>
    ''' <param name="keyValue">����ֵ</param>
    ''' <param name="bCreate">������־��ΪTrue��ʱ�����Բ�����ʱ������������ԣ�Ĭ��ΪFalse��</param>
    ''' <returns>�ɹ�����True����֮ΪFalse</returns>
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

#Region "˽�г�Ա"
    'ȡ�õ���Ԫ��
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

    'ȡ�ýڵ�
    Private Function getXmlNode() As Xml.XmlNode
        Return xDoc.SelectSingleNode("//connectionStrings")
    End Function
#End Region


End Class
