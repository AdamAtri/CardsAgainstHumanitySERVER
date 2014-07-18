Imports System.IO
Imports System.Runtime.InteropServices

Public Module ExceptionLogger

    Private ReadOnly EXE_LOC As String = My.Application.Info.DirectoryPath
    Private ReadOnly LOG_DIR As String = EXE_LOC & "/Logs"
    Private ReadOnly LOG_FILE As String = LOG_DIR & String.Format("/LOG_{0}_{1}_{2}.log", Date.Now.Year, Date.Now.Month, Date.Now.Day)
    Private Const MAX_TRIES As Int32 = 5

    Public Sub Initalize()
        If Not Directory.Exists(LOG_DIR) Then
            Try
                Directory.CreateDirectory(LOG_DIR)
            Catch iox As IOException
                Console.WriteLine(iox)
            Catch ex As Exception
                Console.WriteLine(ex)
            End Try
        End If
    End Sub

    Public Function WriteException(ByVal Excpt As Exception, ByVal gameModule As String, _
                                   Optional ByVal gameID As Int32 = -9999, _
                                   Optional ByVal playerID As Int32 = -9999) As Boolean
        Dim t As New Task(Of Boolean)(Function()
                                          Dim writer As StreamWriter = Nothing

                                          Try
                                              If Not File.Exists(LOG_FILE) Then
                                                  File.Create(LOG_FILE)
                                                  writer = File.AppendText(LOG_FILE)
                                                  With writer
                                                      .AutoFlush = True
                                                      .WriteLine("EXCEPTION LOG FILE")
                                                  End With

                                              End If

                                              Dim count As Int32 = 0
                                              While IsFileOpen()
                                                  Threading.Thread.Sleep(1000)
                                                  count += 1
                                                  If count = MAX_TRIES Then
                                                      Throw New Exception("EXCEPTION LOGGER: Cannot write to exception log file.")
                                                  End If
                                              End While

                                              writer = File.AppendText(LOG_FILE)
                                              With writer
                                                  .AutoFlush = True
                                                  .WriteLine("____________________________________")
                                                  .WriteLine("DATE: " & DateTime.Now.ToString)
                                                  .WriteLine("GAME MODULE: " & gameModule)
                                                  .WriteLine("EXCEPTION: " & Excpt.Message)
                                                  .WriteLine("SOURCE: " & Excpt.Source)
                                                  .WriteLine("STACK TRACE: " & Excpt.StackTrace)
                                                  .WriteLine(vbCrLf)
                                                  If gameID > -9999 Then
                                                      .WriteLine("GAME_ID: " & gameID)
                                                  End If
                                                  If playerID > -9999 Then
                                                      .WriteLine("PLAYER_ID: " & playerID)
                                                  End If
                                              End With
                                          Catch ex As Exception
                                              Console.WriteLine(ex)
                                          Finally
                                              If writer IsNot Nothing Then
                                                  writer.Close()
                                              End If
                                          End Try
                                          Return True
                                      End Function)
        Return t.Result
    End Function

    Private Function IsFileOpen() As Boolean
        Dim fStream As FileStream = Nothing
        Try
            fStream = File.Open(LOG_FILE, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            fStream.Close()
            Return False
        Catch ex As Exception
            Return True
        End Try

    End Function

End Module
