# In Forno - Applicazione per la gestione di una pizzeria

## Configurazione del Database

Prima di avviare l'applicazione, assicurati di configurare correttamente la connessione al database. Apri il file `web.config` (o `app.config` se stai sviluppando un'applicazione console) e trova la sezione relativa alla connection string. Assicurati di sostituire `DataSource=nome_server` con il nome corretto del server del tuo database.

```xml
<connectionStrings>
    <add name="NomeConnessione" connectionString="Data Source=nome_server;Initial Catalog=nome_database;Integrated Security=True" providerName="System.Data.SqlClient" />
</connectionStrings>
```

## Descrizione
In Forno è un'applicazione web sviluppata con ASP.NET MVC 5.2.9 e Entity Framework 6.2.0 per la gestione di una pizzeria. Si concentra principalmente sull'interazione desktop e utilizza Bootstrap 5.2.3 come libreria per la progettazione dell'interfaccia utente.

## Funzionalità Principali

### Per gli Amministratori:
- **Gestione degli Articoli**: Aggiunta di nuovi articoli al menu della pizzeria.
- **Gestione degli Ordini**: Visualizzazione della lista degli ordini e possibilità di evaderli quando completati.
- **Rapporti di Vendita**: Ottenere il totale degli ordini evasi e l'incasso totale di una giornata specifica.

### Per gli Utenti Registrati:
- **Catalogo di Pizze**: Visualizzazione delle pizze disponibili nel menu.
- **Carrello della Spesa**: Aggiunta delle pizze desiderate al carrello.
- **Conferma dell'Ordine**: Possibilità di confermare l'ordine aggiungendo l'indirizzo di consegna e note opzionali.

## Requisiti Tecnici
- **ASP.NET MVC**: Versione 5.2.9
- **Entity Framework**: Versione 6.2.0
- **Bootstrap**: Versione 5.2.3

## Installazione
1. Clona il repository dell'applicazione.
2. Assicurati di avere installato ASP.NET MVC e Entity Framework nelle versioni specificate.
3. Sosituisci nel file Web.Config il Data Source con il nome corretto del tuo server.
4. Avvia l'applicazione nel tuo ambiente di sviluppo.

## Utilizzo
1. Accedi all'applicazione utilizzando le credenziali appropriate.
2. Esplora le diverse funzionalità disponibili per amministratori e utenti.
3. Gestisci articoli, ordini e rapporti di vendita come amministratore.
4. Esplora il catalogo di pizze, aggiungi articoli al carrello e conferma gli ordini come utente.
