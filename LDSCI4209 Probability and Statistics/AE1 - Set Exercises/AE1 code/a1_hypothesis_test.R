#Hypothesis testing

#List the given values and known mean.
masses <- c(101.28, 99.37, 101.31, 100.05, 101.39, 99.4, 100.42, 99.87, 99.42, 101.37, 99.77, 100.63, 101.22, 102.37, 100.16, 100.53, 99.91, 99.76, 99.36, 102.69, 100.25, 100.31, 101.12, 100.02, 99.43, 100.32, 101.03, 98.7, 99.71, 98.74, 100.33, 101.47, 101.47, 101.65, 100.09, 99.32, 100.5, 98.44, 99.19, 102.2)
mean <- 100

#Give the significance Level (assumed here to be 5%)
LOS = 0.05

#calculate sample mean.
sampleMean <- sum(masses) / length(masses)
print(sampleMean)

#calculate sample standard deviation
sampleStandardDeviation <- sd(masses)
print(sampleStandardDeviation)

#Calculate t statistic
tStat <- ((sampleMean - mean) / (sampleStandardDeviation / sqrt(length(masses))))
print(tStat)

#Calculate degrees of freedom
freeDeg <- (length(masses) - 1)
print(freeDeg)

#calculate p value
pVal <- (pt(tStat, df = freeDeg, lower.tail = FALSE))
print(pVal)

#compare P value with LOS
#2-tailed so LOS is divided by 2
print(LOS/2)
print(pVal)
