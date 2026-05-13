import { Paper, Stack, Text } from '@mantine/core';

interface PointProps {
    name: string;
    eventId: string | null | undefined;
    date: string;
}

export default function Point({name, eventId, date}: PointProps) {
    return (
        <Paper>
           <Stack>
                <Text>{name}</Text>
                <Text>{eventId} x {date}</Text>
            </Stack> 
        </Paper>
    );
}
