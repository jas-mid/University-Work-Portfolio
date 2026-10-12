#Random sample generation and plotting to display central limit theorum

#Part I
lambda = 0.2

#Function to generate the mean of a sample of 20 exponential random integers
sampleMeanGenerator <- function(n){
sample <- mean(rexp(n, rate = lambda))
return (sample)
}

#Part II
densityPlotter <-function(observations, sampleSize){
toPlot <- replicate(observations, sampleMeanGenerator(sampleSize))
plot(density(toPlot), main = "Distribution of Sample Means", xlab = "Sample Mean Value", ylab = "Density")
}

densityPlotter(10000,5)
densityPlotter(10000,10)
densityPlotter(10000,20)
densityPlotter(10000,50)
densityPlotter(10000,100)
