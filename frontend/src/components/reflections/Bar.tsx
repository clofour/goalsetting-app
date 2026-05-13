import { Paper } from '@mantine/core';
import type { ReactNode } from 'react';

interface BarProps {
    children?: ReactNode;
}

export default function Bar({children}: BarProps) {
    return (
        <Paper w="100%" h="100%" shadow="sm" withBorder p="md">
            {children}
        </Paper>
    );
}
