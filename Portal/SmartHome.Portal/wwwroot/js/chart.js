function drawSensorGraph(title, unit, maxValue, labels, data) {
    var context = document.getElementById('chart');
    new Chart(context, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: title,
                data: data,
                borderColor: 'rgba(0, 0, 255, 0.8)',
                fill: false,
                borderWidth: 1
            }]
        },
        options: {
            animation: {
                duration: 0
            },
            elements: {
                line: {
                    tension: 0
                }
            },
            hover: {
                animationDuration: 0
            },
            maintainAspectRatio: false,
            responsiveAnimationDuration: 0,
            scales: {
                xAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: '時刻'
                    },
                }],
                yAxes: [{
                    scaleLabel: {
                        display: true,
                        labelString: unit
                    },
                    ticks: {
                        beginAtZero: true,
                        max: maxValue
                    }
                }]
            }
        }
    });
}