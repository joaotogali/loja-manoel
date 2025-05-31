#!/bin/bash

# Aguarda o SQL Server ficar disponível
echo "Aguardando SQL Server iniciar..."
for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1
    if [ $? -eq 0 ]
    then
        echo "SQL Server pronto"
        break
    else
        echo "Aguardando SQL Server..."
        sleep 2
    fi
done

# Cria o banco de dados se não existir
echo "Verificando banco de dados LojaManoel..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'LojaManoel') BEGIN CREATE DATABASE LojaManoel; PRINT 'Banco LojaManoel criado com sucesso.'; END ELSE BEGIN PRINT 'Banco LojaManoel já existe.'; END"

