# BankingSystem-API
Documentation for the BankingSystem API

# Balance Increased Event
You can subscribe to the **onBalanceIncreased** event with ```Game4Freak.BankingSystem.API.onBalanceIncreased```

# Using API functions
You can get the api with ```Game4Freak.BankingSystem.BankingSystem.Instance.api```

## Getting Balance
You can use the function **getPlayerBalance(string playerId)** to get the balance of the players active account  
**OR**  
Use the function **getAccountBalance(string accountId)** to get the balance of a specific account  
or when you got the **AccountData** you can get the variable balance (it doesnt update itself so keep track of it)  

## Changing Balance
You can use the function **increaseBalance(String playerId, decimal amount, TransferReason reason)** to increase / decrease the balance of the players active account  
**OR**  
Use the function **increaseAccountBalance(string accountId, decimal amount, TransferReason reason)** to increase / decrease the balance of a specific account  
**Note:** You can use your custom TransferReason class with your custom text  

## Accounts
### Getting AccountData
You can use the function **getActiveAccount(string playerId)** to get the AccountData of a active account of a player  
**OR**  
Use the function **getAccount(string accountId)** to get the AccountData of a specific account  
**OR**  
Use the function **getAccounts(string playerId)** to get a AccountData list of all accounts of a player  
**OR**  
Use the function **getUserAccounts(string playerId)** to get a AccountData list of all user accounts of a player  
**OR**  
Use the function **getGroupAccounts(string playerId)** to get a AccountData list of all group accounts of a player  

### Editing Accounts
You can use the function **changeAccountName(string accountId, string name)** to change the name of a account  
**OR**  
Use the function **addUserToAccount(string playerId, string accountId)** to add a user to a group account  
**OR**  
Use the function **removeUserFromAccount(string playerId, string accountId)** to remove a user from a group account  

### Creating Account
You can use the function **createAccount(string playerId, string accountName, bool isGroupAccount)** to create a account  
It will return you the AccountData of the created account  

### Account History
You can use the function **getAccountHistory(string accountId)** to get a string list with the account history  
