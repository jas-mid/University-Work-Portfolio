#Chi-Squared test for colours and drinks.

#Create a matrix of the values to represent the table
numMatrix <- matrix( c(65, 40, 35, 60, 30, 20, 25, 25, 25, 20, 40, 15), nrow=4, ncol=3)

#Perform the test.
chisq.test(numMatrix, correct = FALSE)
