Public Class Form1
    ' Instanciar el modelo EDM
    Dim modelo As New Negocios2015Entities

    Sub PaisListar()
        cboPais.DataSource = modelo.usp_PaisListar_EDM
        cboPais.DisplayMember = "NombrePais"
        cboPais.ValueMember = "IdPais"
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PaisListar()
        dgClientes.DataSource = modelo.usp_ClienteListado_EDM
        dgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Sub dgClientes_SelectionChanged(sender As Object, e As EventArgs) Handles dgClientes.SelectionChanged
        txtCodigo.Text = dgClientes.Rows(dgClientes.CurrentRow.Index).Cells(0).Value()
        txtCliente.Text = dgClientes.Rows(dgClientes.CurrentRow.Index).Cells(1).Value()
        txtDireccion.Text = dgClientes.Rows(dgClientes.CurrentRow.Index).Cells(2).Value()
        cboPais.Text = dgClientes.Rows(dgClientes.CurrentRow.Index).Cells(3).Value()
        txtTelefono.Text = dgClientes.Rows(dgClientes.CurrentRow.Index).Cells(4).Value()
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        For Each objeto As Object In Me.Controls
            If TypeOf objeto Is TextBox Then objeto.Text = String.Empty
        Next
        cboPais.SelectedIndex = -1
        txtCodigo.Focus()
    End Sub

    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnGrabar.Click
        Dim ocliente = From cli In modelo.clientes Where cli.IdCliente = txtCodigo.Text

        If ocliente.Count > 0 Then
            MessageBox.Show("El ID ya se encuentra registrado", "Aviso")
            Exit Sub
        End If
        Dim objCliente As New clientes
        objCliente.IdCliente = txtCodigo.Text
        objCliente.NomCliente = txtCliente.Text
        objCliente.DirCliente = txtDireccion.Text
        objCliente.idpais = cboPais.SelectedValue.ToString
        objCliente.fonoCliente = txtTelefono.Text
        objCliente.Activo = True
        Try
            modelo.clientes.Add(objCliente)
            modelo.SaveChanges()
            ' Esto es para que actualice la grilla
            dgClientes.DataSource = modelo.usp_ClienteListado_EDM
            dgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            MessageBox.Show("CLiente registrado!!", "Aviso")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim cliente = From cli In modelo.clientes Where cli.IdCliente = txtCodigo.Text

        Dim objCliente As New clientes
        objCliente = cliente.First
        'objCliente.IdCliente = txtCodigo.Text
        objCliente.NomCliente = txtCliente.Text
        objCliente.DirCliente = txtDireccion.Text
        objCliente.idpais = cboPais.SelectedValue.ToString
        objCliente.fonoCliente = txtTelefono.Text
        objCliente.Activo = True
        Try
            modelo.SaveChanges()
            dgClientes.DataSource = modelo.usp_ClienteListado_EDM
            dgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            MessageBox.Show("CLiente actualizado!!", "Aviso")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnBaja_Click(sender As Object, e As EventArgs) Handles btnBaja.Click
        Dim cliente = From cli In modelo.clientes Where cli.IdCliente = txtCodigo.Text

        Dim objCliente As New clientes
        objCliente = cliente.First
        objCliente.Activo = False
        Try
            modelo.SaveChanges()
            dgClientes.DataSource = modelo.usp_ClienteListado_EDM
            dgClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            MessageBox.Show("Cliente dado de baja", "Aviso")
        Catch ex As Exception

        End Try

    End Sub
End Class
