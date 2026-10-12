#Assigning the list of values to a variable
masses <- c(101.28, 99.37, 101.31, 100.05, 101.39, 99.4, 100.42, 99.87, 99.42, 101.37, 99.77, 100.63, 101.22, 102.37, 100.16, 100.53, 99.91, 99.76, 99.36, 102.69, 100.25, 100.31, 101.12, 100.02, 99.43, 100.32, 101.03, 98.7, 99.71, 98.74, 100.33, 101.47, 101.47, 101.65, 100.09, 99.32, 100.5, 98.44, 99.19, 102.2)

#Creating bar sizes and therefore range
bars <- seq(98, 103, by = 0.83333333333333333333333)

#Creating a histogram with this data
hist(masses, breaks = bars)

dev.off()
